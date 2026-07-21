var BACKEND_URL = "http://localhost:5000";
var lang = "hu";
var mode = "alt";
var langCache = {};

function strLength(str) {
    if (str === "") return 0;
    return 1 + strLength(str.slice(1));
}

function containsDigit(str) {
    if (str === "") return false;
    var c = str[0];
    if (c >= '0' && c <= '9') return true;
    return containsDigit(str.slice(1));
}

function isValidNeptun(str) {
    return /^[A-Za-z0-9]{6}$/.test(str);
}

function sumList(lst) {
    if (lst.length === 0) return 0;
    return lst[0] + sumList(lst.slice(1));
}

function hasZero(lst) {
    if (lst.length === 0) return false;
    if (lst[0] === 0) return true;
    return hasZero(lst.slice(1));
}

function getGrade(score) {
    var t = langCache[lang] || {};
    if (score <= 25) return t.elegtelenLabel || "Elégtelen (1)";
    if (score <= 32) return t.elegsegessLabel || "Elégséges (2)";
    if (score <= 38) return t.kozepesLabel || "Közepes (3)";
    if (score <= 44) return t.joLabel || "Jó (4)";
    return t.jelesLabel || "Jeles (5)";
}

function getPointsArray(index, max) {
    if (index > max) return [];
    var input = document.getElementById("pont" + index);
    var val = 0;
    if (input !== null && input.value !== "") {
        val = parseFloat(input.value);
    }
    return [val].concat(getPointsArray(index + 1, max));
}

function updateSliderFill(input) {
    var pct = (parseFloat(input.value) / parseFloat(input.max)) * 100;
    input.style.background = "linear-gradient(to right, #4964e7 " + pct + "%, #e0e7ff " + pct + "%)";
}

function updateScore() {
    var pts = getPointsArray(1, 10);
    var total = hasZero(pts) ? 0 : Math.round(sumList(pts) * 10) / 10;
    var t = langCache[lang] || {};
    var grade = hasZero(pts) ? (t.elegtelenLabel || "Elégtelen (1)") : getGrade(total);

    document.getElementById("osszpontszam").textContent = total;
    document.getElementById("javaslat").textContent = grade;
    var stickyP = document.getElementById("osszpontszam-sticky");
    var stickyJ = document.getElementById("javaslat-sticky");
    if (stickyP) stickyP.textContent = total;
    if (stickyJ) stickyJ.textContent = grade;

    for (var i = 1; i <= 10; i++) {
        var inp = document.getElementById("pont" + i);
        var kijelzo = document.getElementById("ertek" + i);
        if (inp && kijelzo) {
            kijelzo.textContent = parseFloat(inp.value);
            updateSliderFill(inp);
        }
    }
}

function attachListeners(index, max) {
    if (index > max) return;
    var input = document.getElementById("pont" + index);
    if (input !== null) {
        input.addEventListener("input", updateScore);
    }
    attachListeners(index + 1, max);
}

function ujKerdesHozzaad() {
    var lista = document.getElementById("kerdesek-lista");
    var sorszam = lista.children.length + 1;
    var li = document.createElement("li");
    li.className = "kerdes-sor";
    var input = document.createElement("input");
    input.type = "text";
    input.className = "kerdes-input";
    var t = langCache[lang] || {};
    input.placeholder = sorszam + ". " + (t.kerdesPlaceholder || "kérdés");
    var btn = document.createElement("button");
    btn.type = "button";
    btn.className = "kerdes-torol";
    btn.textContent = "×";
    btn.onclick = function() { li.remove(); };
    li.appendChild(input);
    li.appendChild(btn);
    lista.appendChild(li);
}

function getKerdesek() {
    var inputs = document.querySelectorAll(".kerdes-input");
    var kerdesek = [];
    inputs.forEach(function(inp) {
        if (inp.value.trim() !== "") {
            kerdesek.push(inp.value.trim());
        }
    });
    return kerdesek;
}

function getErtekelesek() {
    var ertekelesek = [];
    for (var i = 1; i <= 10; i++) {
        var pontInput = document.getElementById("pont" + i);
        var megjegyzesInput = document.getElementById("megjegyzes" + i);
        ertekelesek.push({
            id: i,
            jegy: pontInput ? parseFloat(pontInput.value) : 0,
            megjegyzes: megjegyzesInput ? megjegyzesInput.value.trim() : ""
        });
    }
    return ertekelesek;
}

function getAllData() {
    var nev = document.getElementById("nev").value.trim();
    var neptun = document.getElementById("kod").value.trim();
    var szak = parseInt(document.getElementById("szak").value);
    var cim = document.getElementById("cim").value.trim();
    var szerepkor = parseInt(document.getElementById("szerepkor").value);
    var t = langCache[lang] || {};

    if (nev === "") {
        alert(t.validNevKotelezo || "A hallgató neve kötelező!");
        return null;
    }
    if (containsDigit(nev)) {
        alert(t.validNevSzam || "A hallgató neve nem tartalmazhat számot!");
        return null;
    }
    if (!isValidNeptun(neptun)) {
        alert(t.validNeptun || "A Neptun kód pontosan 6 karakter lehet, csak betű és szám!");
        return null;
    }
    if (cim === "") {
        alert(t.validCim || "A szakdolgozat címe kötelező!");
        return null;
    }

    var pts = getPointsArray(1, 10);
    var total = hasZero(pts) ? 0 : Math.round(sumList(pts) * 10) / 10;
    var javasoltErdemjegy = hasZero(pts) ? (t.elegtelenLabel || "Elégtelen (1)") : getGrade(total);

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
        ertekeloSzerepe: szerepkor,
        oldalNyelve: lang === "hu" ? 0 : 1,
        laptipusa: mode === "alt" ? 0 : 1
    };
}

function fetchTranslations(targetLang, callback) {
    if (langCache[targetLang]) {
        callback(langCache[targetLang]);
        return;
    }
    var nyelvErtek = targetLang === "hu" ? 0 : 1;
    fetch(BACKEND_URL + "/szakdolgozatErtekelo/Adatok/get-lokalizacio", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ Nyelv: nyelvErtek })
    })
    .then(function(r) {
        if (!r.ok) throw new Error("HTTP " + r.status);
        return r.text();
    })
    .then(function(text) {
        var t;
        try {
            t = JSON.parse(text);
        } catch(e) {
            t = Function('"use strict"; return ({' + text + '})')();
            t = t[targetLang] || t;
        }
        langCache[targetLang] = t;
        callback(t);
    })
    .catch(function(e) {
        console.error("Lokalizáció betöltése sikertelen:", e);
    });
}

function applyTranslations(t) {
    var el = function(id) { return document.getElementById(id); };

    document.title = t.pageTitle || document.title;
    document.documentElement.lang = lang;

    if (el("header-cim")) el("header-cim").textContent = t.headerCim;
    if (el("szerepkor")) {
        el("szerepkor").options[0].text = t.szerepkorKonzulens;
        el("szerepkor").options[1].text = t.szerepkorOpponens;
    }
    if (el("hallgato-adatai-cim")) el("hallgato-adatai-cim").textContent = t.hallgatoAdatai;
    if (el("nev-label")) el("nev-label").innerHTML = t.hallgatoNeve + ' <span class="kotelezo">*</span>';
    if (el("nev")) el("nev").placeholder = lang === "hu" ? "Pl. Kovács János" : "e.g. John Smith";
    if (el("kod-label")) el("kod-label").innerHTML = t.hallgatoNeptun + ' <span class="kotelezo">*</span>';
    if (el("kod")) el("kod").placeholder = lang === "hu" ? "6 karakter (pl. AB1234)" : "6 characters (e.g. AB1234)";
    if (el("szak-label")) el("szak-label").innerHTML = t.hallgatoSzak + ' <span class="kotelezo">*</span>';
    if (el("szak")) {
        el("szak").options[0].text = t.szakProgramtervezo;
        el("szak").options[1].text = t.szakGazdasagi;
    }
    if (el("cim-label")) el("cim-label").innerHTML = t.hallgatoCim + ' <span class="kotelezo">*</span>';
    if (el("cim")) el("cim").placeholder = lang === "hu" ? "A szakdolgozat teljes címe" : "Full title of the thesis";
    if (el("ertekelesek-cim")) el("ertekelesek-cim").textContent = t.ertekelesekCim;
    if (el("szoveges-ertekeles-cim")) el("szoveges-ertekeles-cim").textContent = t.szovegesErtekelesCim;
    if (el("rovidErtekeles")) el("rovidErtekeles").placeholder = t.szovegesErtekelesPlaceholder;
    if (el("biraloi-javaslat-cim")) el("biraloi-javaslat-cim").textContent = t.biraloiJavaslatkCim;
    if (el("bitraloiJavaslat")) el("bitraloiJavaslat").placeholder = t.biraloiJavaslatkPlaceholder;
    if (el("kerdesek-cim")) el("kerdesek-cim").innerHTML = t.kerdesekCim + ' <span class="kotelezo">*</span>';
    if (el("uj-kerdes-gomb")) el("uj-kerdes-gomb").textContent = t.ujKerdesGomb;
    if (el("ossz-label")) el("ossz-label").textContent = t.osszpontszam;
    if (el("javaslat-label")) el("javaslat-label").textContent = t.javasoltErdemjegy;

    if (t.criteria) {
        for (var i = 1; i <= 10; i++) {
            var c = t.criteria[i - 1];
            if (!c) continue;
            if (el("criterion-cim-" + i)) el("criterion-cim-" + i).textContent = c.cim;
            if (el("criterion-leiras-" + i)) el("criterion-leiras-" + i).textContent = c.leiras;
            var ul = el("criterion-pontok-" + i);
            if (ul && c.pontok) {
                var items = ul.querySelectorAll("li");
                for (var j = 0; j < items.length && j < c.pontok.length; j++) {
                    items[j].textContent = c.pontok[j];
                }
            }
            if (el("megjegyzes" + i)) el("megjegyzes" + i).placeholder = t.megjegyzesPlaceholder;
            if (el("pont-kijelzo-label-" + i)) el("pont-kijelzo-label-" + i).textContent = t.adottPont;
        }
    }
}

function updateModeSwitchVisibility() {
    var modeLabel = document.getElementById("modeSwitch");
    if (!modeLabel) return;
    var modeBox = modeLabel.nextElementSibling;
    if (lang === "en") {
        modeLabel.disabled = true;
        if (modeBox) {
            modeBox.style.opacity = "0.38";
            modeBox.style.pointerEvents = "none";
            modeBox.style.cursor = "default";
        }
    } else {
        modeLabel.disabled = false;
        if (modeBox) {
            modeBox.style.opacity = "1";
            modeBox.style.pointerEvents = "";
            modeBox.style.cursor = "pointer";
        }
    }
}

function setLanguage(newLang) {
    lang = newLang;
    updateModeSwitchVisibility();
    fetchTranslations(lang, function(t) {
        applyTranslations(t);
        updateScore();
    });
}

function sendData(endpoint, filename) {
    var data = getAllData();
    if (data === null) return;
    var t = langCache[lang] || {};
    fetch(BACKEND_URL + endpoint, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    })
    .then(function(response) {
        if (!response.ok) throw new Error((t.hibaSzerver || "Szerver hiba: ") + response.status);
        return response.blob();
    })
    .then(function(file) {
        var objUrl = URL.createObjectURL(file);
        var link = document.createElement("a");
        link.href = objUrl;
        link.download = filename;
        link.click();
        URL.revokeObjectURL(objUrl);
    })
    .catch(function(err) {
        alert((t.hibaLetoltes || "Hiba: ") + err.message);
    });
}

function downloadpdf() { sendData("/szakdolgozatErtekelo/Adatok/get-pdf", "Biralat.pdf"); }
function downloadword() { sendData("/szakdolgozatErtekelo/Adatok/get-word", "Biralat.docx"); }
function downloadlatex() { sendData("/szakdolgozatErtekelo/Adatok/get-latex", "Biralat.tex"); }
function downloadzip() { sendData("/szakdolgozatErtekelo/Adatok/get-zip", "Dokumentumok.zip"); }

function neptunSzures() {
    var input = document.getElementById("kod");
    input.value = input.value.replace(/[^A-Za-z0-9]/g, "").slice(0, 6);
}

window.onload = function() {
    attachListeners(1, 10);
    document.getElementById("kod").addEventListener("input", neptunSzures);

    document.getElementById("langSwitch").addEventListener("change", function() {
        if (this.checked) {
            document.getElementById("langText").textContent = "Angol";
            var modeSwitch = document.getElementById("modeSwitch");
            if (modeSwitch && modeSwitch.checked) {
                modeSwitch.checked = false;
                mode = "alt";
                document.getElementById("modeText").textContent = "Általános";
            }
            setLanguage("en");
        } else {
            document.getElementById("langText").textContent = "Magyar";
            setLanguage("hu");
        }
    });

    document.getElementById("modeSwitch").addEventListener("change", function() {
        if (this.checked) {
            mode = "tud";
            document.getElementById("modeText").textContent = "Tudományos";
        } else {
            mode = "alt";
            document.getElementById("modeText").textContent = "Általános";
        }
    });

    setLanguage("hu");
};
