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

function isEmpty(str) {
    if (str === "") {
        return true;
    } else {
        return false;
    }
}

function showErrorAndFocus(msg, elemId) {
    alert(msg);
    let elem = document.getElementById(elemId);
    if (elem !== null) {
        elem.focus();
        elem.scrollIntoView();
    }
    return null;
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

function getFirstMissingPointIndex(index, max) {
    if (index > max) {
        return 0;
    } else {
        let input = document.getElementById("pont" + index);
        if (input === null || input.value === "") {
            return index;
        } else {
            return getFirstMissingPointIndex(index + 1, max);
        }
    }
}

function getAnswers(index, max) {
    if (index > max) {
        return [];
    } else {
        let input = document.getElementById("indok" + index);
        let val = "";
        if (input !== null) {
            val = input.value;
        }
        return [val].concat(getAnswers(index + 1, max));
    }
}

function getQuestionsList(nodes) {
    if (nodes.length === 0) {
        return [];
    } else {
        let input = nodes[0].querySelector("input");
        let val = "";
        if (input !== null) {
            val = input.value;
        }
        return [val].concat(getQuestionsList(Array.prototype.slice.call(nodes, 1)));
    }
}

function focusEmptyQuestion(nodes) {
    if (nodes.length === 0) {
        return false;
    } else {
        let input = nodes[0].querySelector("input");
        if (input !== null && isEmpty(input.value)) {
            alert("Hiba: A hozzáadott kérdések nem lehetnek üresek!");
            input.focus();
            input.scrollIntoView();
            return true;
        } else {
            return focusEmptyQuestion(Array.prototype.slice.call(nodes, 1));
        }
    }
}

function buildErtekelesek(pts, indokok, idx) {
    if (pts.length === 0) {
        return [];
    } else {
        let obj = {
            KriteriumId: idx,
            Pontszam: pts[0],
            Indoklas: indokok[0]
        };
        return [obj].concat(buildErtekelesek(pts.slice(1), indokok.slice(1), idx + 1));
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
    
    let pontMezo = document.getElementById("osszpontszam");
    if (pontMezo !== null) {
        pontMezo.value = total;
    }
    
    let javaslatMezo = document.getElementById("javaslat");
    if (javaslatMezo !== null) {
        javaslatMezo.value = getGrade(total);
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

function getAllData(formatumKod) {
    let name = "";
    let nevElem = document.getElementById("nev");
    if (nevElem !== null) {
        name = nevElem.value;
    }
    
    let neptun = "";
    let neptunElem = document.getElementById("kod");
    if (neptunElem !== null) {
        neptun = neptunElem.value;
    }

    let szak = "";
    let szakNode = document.getElementById("szak");
    if (szakNode !== null) {
        szak = szakNode.value;
    }

    let cim = "";
    let cimNode = document.getElementById("cim");
    if (cimNode !== null) {
        cim = cimNode.value;
    }

    if (isEmpty(name)) {
        return showErrorAndFocus("Hiba: A név hiányzik!", "nev");
    }
    if (containsDigit(name)) {
        return showErrorAndFocus("Hiba: A név nem tartalmazhat számot!", "nev");
    }
    
    if (isEmpty(neptun)) {
        return showErrorAndFocus("Hiba: A Neptun kód hiányzik!", "kod");
    }
    if (strLength(neptun) !== 6) {
        return showErrorAndFocus("Hiba: A Neptun kód pontosan 6 karakter kell, hogy legyen!", "kod");
    }

    if (isEmpty(szak)) {
        return showErrorAndFocus("Hiba: A szak hiányzik!", "szak");
    }
    
    if (isEmpty(cim)) {
        return showErrorAndFocus("Hiba: A cím hiányzik!", "cim");
    }

    let missingIndex = getFirstMissingPointIndex(1, 10);
    if (missingIndex !== 0) {
        return showErrorAndFocus("Hiba: A(z) " + missingIndex + ". értékelési szempontnál hiányzik a pontszám!", "pont" + missingIndex);
    }

    let ul = document.getElementById("lista");
    let kerdesekList = [];
    if (ul !== null) {
        kerdesekList = getQuestionsList(ul.children);
    }

    if (kerdesekList.length === 0) {
        alert("Hiba: Legalább egy kérdést meg kell adni a hallgatónak!");
        if (ul !== null) {
            ul.scrollIntoView();
        }
        return null;
    }

    if (ul !== null && focusEmptyQuestion(ul.children)) {
        return null;
    }

    let pts = getPointsArray(1, 10);
    let indokok = getAnswers(1, 10);
    let ertekelesekList = buildErtekelesek(pts, indokok, 1);
    
    let total = 0;
    if (hasZero(pts)) {
        total = 0;
    } else {
        total = sumList(pts);
    }

    let ertekeles = "";
    let ertekelesNode = document.getElementById("ertekeles");
    if (ertekelesNode !== null) {
        ertekeles = ertekelesNode.value;
    }

    let javaslat = "";
    let javaslatNode = document.getElementById("biralas");
    if (javaslatNode !== null) {
        javaslat = javaslatNode.value;
    }

    return {
        Hallgato: {
            Nev: name,
            Neptun: neptun,
            Szak: szak,
            SzakdolgozatCime: cim
        },
        Ertekelesek: ertekelesekList,
        OsszesitettErtekeles: total,
        RovidSzovegesErtekeles: ertekeles,
        JavasoltErdemjegy: getGrade(total),
        BitraloiJavaslat: javaslat,
        Kerdesek: kerdesekList,
        ErtekeloSzerepe: 0,
        Formatum: formatumKod
    };
}

function preventFormSubmit() {
    if (typeof event !== 'undefined' && event.preventDefault) {
        event.preventDefault();
    }
}

function sendData(url, filename, formatumKod) {
    preventFormSubmit();
    let data = getAllData(formatumKod);
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
    if (e) { e.preventDefault(); }
    sendData("/api/pdf", "adatok.pdf", 0);
}

function downloadxml(e) {
    if (e) { e.preventDefault(); }
    sendData("/api/xml", "adatok.xml", 1);
}

function downloadlatex(e) {
    if (e) { e.preventDefault(); }
    sendData("/api/latex", "adatok.tex", 2);
}