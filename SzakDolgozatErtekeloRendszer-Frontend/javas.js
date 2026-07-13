function strLength(str) {
    if (str === "") {
        return 0;
    } else {
        return 1 + strLength(str.slice(1));
    }
}

function isDigit(c) {
    return c >= '0' && c <= '9';
}

function containsDigit(str) {
    if (str === "") {
        return false;
    } else {
        if (isDigit(str[0])) {
            return true;
        } else {
            return containsDigit(str.slice(1));
        }
    }
}

function sumList(lst) {
    if (lst.length === 0) {
        return 0;
    } else {
        return lst[0] + sumList(lst.slice(1));
    }
}

function hasZero(lst) {
    if (lst.length === 0) {
        return false;
    } else {
        if (lst[0] === 0) {
            return true;
        } else {
            return hasZero(lst.slice(1));
        }
    }
}

function getGrade(score) {
    if (score === 0) {
        return "Elégtelen (1)";
    } else if (score >= 8 && score <= 25) {
        return "Elégtelen (1)";
    } else if (score >= 26 && score <= 32) {
        return "Elégséges (2)";
    } else if (score >= 33 && score <= 38) {
        return "Közepes (3)";
    } else if (score >= 39 && score <= 44) {
        return "Jó (4)";
    } else if (score >= 45 && score <= 50) {
        return "Jeles (5)";
    } else {
        return "-";
    }
}

function getPointsArray(index, max) {
    if (index > max) {
        return [];
    } else {
        let input = document.getElementById("pont" + index);
        let val = 0;
        if (input !== null && input.value !== "") {
            val = parseFloat(input.value);
        }
        return [val].concat(getPointsArray(index + 1, max));
    }
}

function updateSliderFill(input) {
    let pct = (parseFloat(input.value) / parseFloat(input.max)) * 100;
    input.style.background = "linear-gradient(to right, #4964e7 " + pct + "%, #e0e7ff " + pct + "%)";
}

function updateScore() {
    let pts = getPointsArray(1, 10);
    let total = Math.round(sumList(pts) * 10) / 10;
    let grade = hasZero(pts) ? "Elégtelen (1)" : getGrade(total);

    document.getElementById("osszpontszam").textContent = total;
    document.getElementById("javaslat").textContent = grade;
    document.getElementById("osszpontszam-sticky").textContent = total;
    document.getElementById("javaslat-sticky").textContent = grade;

    for (let i = 1; i <= 10; i++) {
        let inp = document.getElementById("pont" + i);
        let kijelzo = document.getElementById("ertek" + i);
        if (inp && kijelzo) {
            kijelzo.textContent = parseFloat(inp.value);
            updateSliderFill(inp);
        }
    }
}

function attachListeners(index, max) {
    if (index > max) {
        return;
    } else {
        let input = document.getElementById("pont" + index);
        if (input !== null) {
            input.addEventListener("input", updateScore);
        }
        attachListeners(index + 1, max);
    }
}

function ujKerdesHozzaad() {
    let lista = document.getElementById("kerdesek-lista");
    let sorszam = lista.children.length + 1;
    let li = document.createElement("li");
    let input = document.createElement("input");
    input.type = "text";
    input.className = "kerdes-input";
    input.placeholder = sorszam + ". kérdés";
    li.appendChild(input);
    lista.appendChild(li);
}

function getKerdesek() {
    let inputs = document.querySelectorAll(".kerdes-input");
    let kerdesek = [];
    inputs.forEach(function(inp) {
        if (inp.value.trim() !== "") {
            kerdesek.push(inp.value.trim());
        }
    });
    return kerdesek;
}

function getErtekelesek() {
    let ertekelesek = [];
    for (let i = 1; i <= 10; i++) {
        let pontInput = document.getElementById("pont" + i);
        let megjegyzesInput = document.getElementById("megjegyzes" + i);
        ertekelesek.push({
            id: i,
            jegy: pontInput ? parseFloat(pontInput.value) : 0,
            megjegyzes: megjegyzesInput ? megjegyzesInput.value.trim() : ""
        });
    }
    return ertekelesek;
}

function getAllData() {
    let nev = document.getElementById("nev").value.trim();
    let neptun = document.getElementById("kod").value.trim();
    let szak = parseInt(document.getElementById("szak").value);
    let cim = document.getElementById("cim").value.trim();
    let szerepkor = parseInt(document.getElementById("szerepkor").value);

    if (nev === "") {
        alert("A hallgató neve kötelező!");
        return null;
    }
    if (containsDigit(nev)) {
        alert("A hallgató neve nem tartalmazhat számot!");
        return null;
    }
    if (strLength(neptun) !== 6) {
        alert("A Neptun kód pontosan 6 karakter kell legyen!");
        return null;
    }
    if (cim === "") {
        alert("A szakdolgozat címe kötelező!");
        return null;
    }

    let pts = getPointsArray(1, 10);
    let total = 0;
    if (hasZero(pts)) {
        total = 0;
    } else {
        total = Math.round(sumList(pts) * 10) / 10;
    }

    let javasoltErdemjegy = getGrade(total);

    return {
        hallgato: {
            nev: nev,
            netpun: neptun,
            szak: szak,
            szakdolgozatCime: cim
        },
        ertekelesek: getErtekelesek(),
        osszesitettErtekeles: total,
        rovidSzovegesErtekeles: document.getElementById("rovidErtekeles").value.trim(),
        javasoltErdemjegy: javasoltErdemjegy,
        bitraloiJavaslat: document.getElementById("bitraloiJavaslat").value.trim(),
        kerdesek: getKerdesek(),
        ertekeloSzerepe: szerepkor
    };
}

const BACKEND_URL = "http://localhost:5039/";

function sendData(endpoint, filename) {
    let data = getAllData();
    if (data === null) {
        return;
    }

    fetch(BACKEND_URL + endpoint, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
    .then(function(response) {
        if (!response.ok) {
            throw new Error("A szerver hibát adott vissza: " + response.status);
        }
        return response.blob();
    })
    .then(function(file) {
        let objUrl = URL.createObjectURL(file);
        let link = document.createElement("a");
        link.href = objUrl;
        link.download = filename;
        link.click();
        URL.revokeObjectURL(objUrl);
    })
    .catch(function(err) {
        alert("Hiba a letöltés során: " + err.message);
    });
}

function downloadpdf() {
    sendData("/szakdolgozatErtekelo/Adatok/get-pdf", "Biralat.pdf");
}

function downloadword() {
    sendData("/szakdolgozatErtekelo/Adatok/get-word", "Biralat.docx");
}

function downloadlatex() {
    sendData("/szakdolgozatErtekelo/Adatok/get-latex", "Biralat.tex");
}

function downloadzip() {
    sendData("/szakdolgozatErtekelo/Adatok/get-zip", "Dokumentumok.zip");
}

window.onload = function() {
    attachListeners(1, 10);
    updateScore();
};