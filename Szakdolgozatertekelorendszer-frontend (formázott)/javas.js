function strLength(str) {
    if (str === "") {
        return 0;
    } else {
        return 1 + strLength(str.slice(1));
    }
}

function isDigit(c) {
    if (c >= '0' && c <= '9') {
        return true;
    } else {
        return false;
    }
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
        return "";
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

function updateScore() {
    let pts = getPointsArray(1, 10);
    let total = 0;
    if (hasZero(pts)) {
        total = 0;
    } else {
        total = sumList(pts);
    }
    document.getElementById("osszpontszam").textContent = total;
    document.getElementById("javaslat").textContent = getGrade(total);
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

window.onload = function() {
    attachListeners(1, 10);
};

function Add() {
    let li = document.createElement("li");
    let input = document.createElement("input");
    input.type = "text";
    li.appendChild(input);
    document.getElementById("lista").appendChild(li);
}

function getAllData() {
    let name = document.getElementById("nev").value;
    let neptun = document.getElementById("kod").value;
    
    if (containsDigit(name)) {
        alert("A nev nem tartalmazhat szamot!");
        return null;
    }
    if (strLength(neptun) !== 6) {
        alert("A neptun kod pontosan 6 karakter kell legyen!");
        return null;
    }

    return {
        nev: name,
        kod: neptun,
        szak: document.getElementById("szak").value,
        cim: document.getElementById("cim").value,
        pontok: getPointsArray(1, 10),
        osszpont: document.getElementById("osszpontszam").value
    };
}

function sendData(url, filename) {
    let data = getAllData();
    if (data === null) {
        return;
    }
    
    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    }).then(function(response) {
        return response.blob();
    }).then(function(file) {
        let objUrl = URL.createObjectURL(file);
        let link = document.createElement("a");
        link.href = objUrl;
        link.download = filename;
        link.click();
        URL.revokeObjectURL(objUrl);
    });
}

function downloadpdf(e) {
    if (event) { event.preventDefault(); }
    sendData("/api/pdf", "adatok.pdf");
}

function downloadxml(e) {
    if (event) { event.preventDefault(); }
    sendData("/api/xml", "adatok.xml");
}

function downloadlatex(e) {
    if (event) { event.preventDefault(); }
    sendData("/api/latex", "adatok.tex");
}