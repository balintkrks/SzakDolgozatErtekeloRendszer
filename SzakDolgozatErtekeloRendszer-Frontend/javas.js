function strLength(str) {
    if (str === "") {
        return 0;
    } else {
        return 1 + strLength(str.slice(1));
    }
}

function containsDigit(str) {
    if (str === "") return false;
    let c = str[0];
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
    if (score <= 25) return i18n[lang].elegtelenLabel;
    if (score <= 32) return i18n[lang].elegsegessLabel;
    if (score <= 38) return i18n[lang].kozepesLabel;
    if (score <= 44) return i18n[lang].joLabel;
    return i18n[lang].jelesLabel;
}

function getPointsArray(index, max) {
    if (index > max) return [];
    let input = document.getElementById("pont" + index);
    let val = 0;
    if (input !== null && input.value !== "") {
        val = parseFloat(input.value);
    }
    return [val].concat(getPointsArray(index + 1, max));
}

function updateSliderFill(input) {
    let pct = (parseFloat(input.value) / parseFloat(input.max)) * 100;
    input.style.background = "linear-gradient(to right, #4964e7 " + pct + "%, #e0e7ff " + pct + "%)";
}

function updateScore() {
    let pts = getPointsArray(1, 10);
    let total = hasZero(pts) ? 0 : Math.round(sumList(pts) * 10) / 10;
    let grade = hasZero(pts) ? i18n[lang].elegtelenLabel : getGrade(total);

    document.getElementById("osszpontszam").textContent = total;
    document.getElementById("javaslat").textContent = grade;
    let stickyP = document.getElementById("osszpontszam-sticky");
    let stickyJ = document.getElementById("javaslat-sticky");
    if (stickyP) stickyP.textContent = total;
    if (stickyJ) stickyJ.textContent = grade;

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
    if (index > max) return;
    let input = document.getElementById("pont" + index);
    if (input !== null) {
        input.addEventListener("input", updateScore);
    }
    attachListeners(index + 1, max);
}

function ujKerdesHozzaad() {
    let lista = document.getElementById("kerdesek-lista");
    let sorszam = lista.children.length + 1;
    let li = document.createElement("li");
    li.className = "kerdes-sor";
    let input = document.createElement("input");
    input.type = "text";
    input.className = "kerdes-input";
    input.placeholder = sorszam + ". " + i18n[lang].kerdesPlaceholder;
    let btn = document.createElement("button");
    btn.type = "button";
    btn.className = "kerdes-torol";
    btn.textContent = "×";
    btn.onclick = function() { li.remove(); };
    li.appendChild(input);
    li.appendChild(btn);
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
        alert(i18n[lang].validNevKotelezo);
        return null;
    }
    if (containsDigit(nev)) {
        alert(i18n[lang].validNevSzam);
        return null;
    }
    if (!isValidNeptun(neptun)) {
        alert(i18n[lang].validNeptun);
        return null;
    }
    if (cim === "") {
        alert(i18n[lang].validCim);
        return null;
    }

    let pts = getPointsArray(1, 10);
    let total = hasZero(pts) ? 0 : Math.round(sumList(pts) * 10) / 10;
    let javasoltErdemjegy = hasZero(pts) ? i18n[lang].elegtelenLabel : getGrade(total);

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
        nyelv: lang
    };
}

var lang = "hu";

var i18n = {
    hu: {
        pageTitle: "Szakdolgozati Bírálati Lap",
        headerCim: "Szakdolgozati Bírálati Lap",
        szerepkorKonzulens: "Konzulens",
        szerepkorOpponens: "Opponens",
        hallgatoAdatai: "Hallgató adatai",
        hallgatoNeve: "A hallgató neve:",
        hallgatoNeptun: "A hallgató Neptun kódja:",
        hallgatoSzak: "A hallgató szakja:",
        hallgatoCim: "A szakdolgozat címe:",
        szakProgramtervezo: "Programtervező informatikus",
        szakGazdasagi: "Gazdasági Informatikus",
        ertekelesekCim: "Értékelési szempontok",
        adottPont: "Adott pont:",
        megjegyzesPlaceholder: "Rövid indoklás (opcionális)",
        szovegesErtekelesCim: "A dolgozat rövid szöveges értékelése (opcionális)",
        szovegesErtekelesPlaceholder: "Írja be a rövid szöveges értékelést...",
        biraloiJavaslatkCim: "A bíráló javaslatai a védéshez (opcionális)",
        biraloiJavaslatkPlaceholder: "Írja be a védéshez kapcsolódó javaslatokat...",
        kerdesekCim: "A hallgató által megválaszolandó kérdések",
        kerdesPlaceholder: "kérdés",
        ujKerdesGomb: "+ Új kérdés hozzáadása",
        osszpontszam: "Összpontszám:",
        javasoltErdemjegy: "Javasolt érdemjegy:",
        pdfGomb: "📄 PDF letöltése",
        wordGomb: "📝 Word letöltése",
        latexGomb: "🔤 LaTeX letöltése",
        zipGomb: "📦 ZIP letöltése",
        elegtelenLabel: "Elégtelen (1)",
        elegsegessLabel: "Elégséges (2)",
        kozepesLabel: "Közepes (3)",
        joLabel: "Jó (4)",
        jelesLabel: "Jeles (5)",
        validNevKotelezo: "A hallgató neve kötelező!",
        validNevSzam: "A hallgató neve nem tartalmazhat számot!",
        validNeptun: "A Neptun kód pontosan 6 karakter lehet, csak betű és szám!",
        validCim: "A szakdolgozat címe kötelező!",
        hibaSzerver: "A szerver hibát adott vissza: ",
        hibaLetoltes: "Hiba a letöltés során: ",
        criteria: [
            {
                cim: "A szakdolgozat szerkezeti felépítése, tartalmi tagolása, alaki megjelenése.",
                leiras: "A dolgozat szerkezeti felépítése és alaki megjelenése.",
                pontok: [
                    "0: A dolgozat nem felel meg az EKKE szakdolgozati követelményeinek",
                    "1-2: A követelményeknek lényegében megfelel, de nagyobb hiányosságokkal.",
                    "3-4: A követelményeknek lényegében megfelel, kisebb hiányosságokkal.",
                    "5: Kifogástalan szerkezet, tartalmi tagolás, szép kivitel."
                ]
            },
            {
                cim: "Nyelvezet és stílus",
                leiras: "A dolgozat nyelvezete és stílusa.",
                pontok: [
                    "0: Nyelvezete mondatszerkesztése erősen kifogásolható, durva helyesírási hibákat tartalmaz.",
                    "1-2: Nyelvezet, stílusa sok hiányossággal, kisebb helyesírási hibákat tartalmaz.",
                    "3-4: Nyelvezete megfelelő, kevés stílushibával.",
                    "5: Kifogástalan nyelvezet és stílus."
                ]
            },
            {
                cim: "Szakirodalmi háttér",
                leiras: "A felhasznált szakirodalom minősége.",
                pontok: [
                    "0: A szakirodalom nem releváns. A hivatkozások hiányoznak, vagy nem szakszerűek.",
                    "1-2: A szakirodalom feltárása szűk körű, csak tankönyvek, vagy jegyzetek anyagát tartalmazza.",
                    "3-4: Legfontosabb szakirodalmakat, korrekt hivatkozásokat tartalmaz a dolgozat.",
                    "5: Legújabb széles körű szakirodalom alapján íródott a dolgozat. Hivatkozások pontosak."
                ]
            },
            {
                cim: "A megvalósított feladat nehézsége",
                leiras: "A szakmai színvonal és bonyolultság.",
                pontok: [
                    "0: A megvalósított feladat nem teljesíti a szakdolgozattól elvárt nehézségi szintet.",
                    "1-2: A megvalósított feladat éppen teljesíti a szakdolgozattól elvárt nehézségi szintet.",
                    "3-4: A megvalósított feladat teljesíti a szakdolgozattól elvárt nehézségi szintet.",
                    "5: A megvalósított feladat túlmutat a szakdolgozattól elvárt nehézségi szinten."
                ]
            },
            {
                cim: "Megvalósítás minősége I.",
                leiras: "A feladat ismertetése, a célkitűzés definiálása (specifikáció, megvalósítási terv, összefüggések elemzése).",
                pontok: [
                    "0: A dolgozatból a feladat ismertetése, illetve a célkitűzés definiálása teljes mértékben hiányzik.",
                    "1-2: A dolgozatban megjelenik a feladat ismertetése, de a leírás alapszintű, elnagyolt, hiányos.",
                    "3-4: A feladat ismertetése formailag megfelelő, egy alapszintű leíráson túlmutat, de nem teljes.",
                    "5: A feladat ismertetése szakmailag korrekt, formailag megfelelő, teljes és ellentmondásmentes."
                ]
            },
            {
                cim: "Megvalósítás minősége II.",
                leiras: "Kódminőség.",
                pontok: [
                    "0: A forráskód tartalma, mennyisége, szerkezete nem megfelelő.",
                    "1-2: A forráskód megfelelő, de nehezen áttekinthető, strukturálatlan.",
                    "3-4: A forráskód tartalma, mennyisége, összetettsége jó. Jól strukturált, áttekinthető.",
                    "5: A forráskód kiváló. Korszerű, hatékony, öndokumentáló, újrafelhasználható."
                ]
            },
            {
                cim: "Megvalósítás minősége III.",
                leiras: "Fejlesztési- és felhasználói dokumentáció.",
                pontok: [
                    "0: A Fejlesztési- és felhasználói dokumentáció nem felel meg a követelményeknek.",
                    "1-2: A dokumentáció elnagyolt, hiányos, szerkezete logikátlan.",
                    "3-4: A dokumentáció tartalmazza a fontosabb fejezeteket, szerkezete logikus.",
                    "5: A dokumentáció kiváló színvonalú, a dokumentálási szabályoknak teljes mértékben megfelel."
                ]
            },
            {
                cim: "Megvalósítás minősége IV.",
                leiras: "Tesztelés, futtatás.",
                pontok: [
                    "0: A dolgozat nem tartalmaz teszteléshez köthető részeket. A szoftver nem futtatható.",
                    "1-2: A dolgozatban megjelennek a teszteléssel kapcsolatos törekvések, de elnagyoltak, hiányosak.",
                    "3-4: Jó színvonalú, de nem teljes körű tesztelés.",
                    "5: Teljes körű és jól dokumentált validáció és verifikáció."
                ]
            },
            {
                cim: "A szakdolgozat összefoglalása.",
                leiras: "",
                pontok: [
                    "0: Zavaros, leíró összefoglalás, tézisek nélkül.",
                    "1-2: Leíró jellegű összefoglalás, elnagyolt.",
                    "3-4: Világos tagolt összefoglalás, korrekt.",
                    "5: Kifogástalan, lényegre törő, továbbtervező."
                ]
            },
            {
                cim: "Összbenyomás, konzulens/opponens véleménye",
                leiras: "",
                pontok: [
                    "0: A dolgozat szakmailag nem releváns, tartalmi formai követelményeknek nem felel meg.",
                    "1-2: A dolgozat szakmailag kevésbé releváns, tartalmi formai követelményeknek megfelel.",
                    "3-4: A dolgozat korrekt szakmai és módszertani felkészültséget tükröz.",
                    "5: A dolgozat kiváló szakmai felkészültséget tükröz."
                ]
            }
        ]
    },
    en: {
        pageTitle: "Thesis Work Review Sheet",
        headerCim: "Thesis Work Review Sheet",
        szerepkorKonzulens: "Consultant",
        szerepkorOpponens: "Opponent",
        hallgatoAdatai: "Student details",
        hallgatoNeve: "Name of the student:",
        hallgatoNeptun: "The student's Neptun code:",
        hallgatoSzak: "The student's major:",
        hallgatoCim: "Title of the thesis:",
        szakProgramtervezo: "Computer Science",
        szakGazdasagi: "Business Informatics",
        ertekelesekCim: "Evaluation criteria",
        adottPont: "Score achieved:",
        megjegyzesPlaceholder: "Short justification (optional)",
        szovegesErtekelesCim: "Short text evaluation of the thesis (optional)",
        szovegesErtekelesPlaceholder: "Enter the short text evaluation...",
        biraloiJavaslatkCim: "Reviewer's suggestions for defense (optional)",
        biraloiJavaslatkPlaceholder: "Enter suggestions for the defense...",
        kerdesekCim: "Questions to be answered by the student",
        kerdesPlaceholder: "question",
        ujKerdesGomb: "+ Add new question",
        osszpontszam: "Total score:",
        javasoltErdemjegy: "Recommended grade:",
        pdfGomb: "📄 Download PDF",
        wordGomb: "📝 Download Word",
        latexGomb: "🔤 Download LaTeX",
        zipGomb: "📦 Download ZIP",
        elegtelenLabel: "Insufficient (1)",
        elegsegessLabel: "Sufficient (2)",
        kozepesLabel: "Medium (3)",
        joLabel: "Good (4)",
        jelesLabel: "Marked (5)",
        validNevKotelezo: "The student's name is required!",
        validNevSzam: "The student's name must not contain digits!",
        validNeptun: "The Neptun code must be exactly 6 characters, letters and digits only!",
        validCim: "The title of the thesis is required!",
        hibaSzerver: "The server returned an error: ",
        hibaLetoltes: "Error during download: ",
        criteria: [
            {
                cim: "The thesis's structure, content division, and appearance.",
                leiras: "The thesis's structure, content division, and appearance.",
                pontok: [
                    "0: The thesis does not meet the thesis requirements of EKKE.",
                    "1-2: It meets the requirements, but with major shortcomings.",
                    "3-4: It essentially meets the requirements, with minor deficiencies.",
                    "5: Impeccable structure, content division, beautiful design."
                ]
            },
            {
                cim: "The language, style and linguistic correctness of the thesis.",
                leiras: "The language, style and linguistic correctness of the thesis.",
                pontok: [
                    "0: Highly objectionable sentence structure, contains gross spelling errors.",
                    "1-2: Language and style with many shortcomings and minor spelling errors.",
                    "3-4: His language is appropriate, with few stylistic errors.",
                    "5: Impeccable language and style."
                ]
            },
            {
                cim: "Exploration of the technical literature background.",
                leiras: "Professionalism and correctness of the references.",
                pontok: [
                    "0: The literature is not relevant. References are missing or not professional.",
                    "1-2: The exploration of the literature is narrow, only includes textbooks or notes.",
                    "3-4: The thesis contains the most important literature and correct references.",
                    "5: The thesis was written based on the latest extensive literature. Links are accurate."
                ]
            },
            {
                cim: "The difficulty and complexity of the implemented task.",
                leiras: "Professional level and complexity.",
                pontok: [
                    "0: The completed task does not meet the level of difficulty expected from the thesis.",
                    "1-2: The completed task meets the level of difficulty expected from the thesis.",
                    "3-4: The completed task fulfills the level of difficulty expected from the thesis.",
                    "5: The completed task goes beyond the level of difficulty expected from a thesis."
                ]
            },
            {
                cim: "Quality of implementation I.",
                leiras: "Description of the task, definition of the objective (specification, implementation plan, analysis of correlations).",
                pontok: [
                    "0: The description of the task and the definition of the objective are completely missing.",
                    "1-2: The thesis contains a description, but it is basic, rough, and incomplete.",
                    "3-4: The description is formally appropriate, beyond basic level, but not complete.",
                    "5: The description is professionally correct, formally appropriate, complete and without contradictions."
                ]
            },
            {
                cim: "Quality of implementation II.",
                leiras: "Code quality.",
                pontok: [
                    "0: The content, quantity, and structure of the source code are not appropriate.",
                    "1-2: The source code is adequate, but difficult to understand, extensive, unstructured.",
                    "3-4: The source code content is good. Modern, efficient language elements. Well structured.",
                    "5: Excellent source code. Modern, effective, well structured, self-documenting, reusable."
                ]
            },
            {
                cim: "Quality of implementation III.",
                leiras: "Development and user documentation.",
                pontok: [
                    "0: The Development and User Documentation does not meet the requirements.",
                    "1-2: The documentation is rough, incomplete, and its structure is illogical.",
                    "3-4: The documentation contains the most important chapters, its structure is logical.",
                    "5: The documentation is of excellent quality and fully complies with the documentation rules."
                ]
            },
            {
                cim: "Quality of implementation IV.",
                leiras: "Testing, running.",
                pontok: [
                    "0: The thesis does not contain parts related to testing. The software does not run.",
                    "1-2: Attempts related to testing appear in the thesis, but they are rough and incomplete.",
                    "3-4: Good quality, but not comprehensive testing.",
                    "5: Comprehensive and well-documented validation and verification."
                ]
            },
            {
                cim: "Summary of the thesis.",
                leiras: "",
                pontok: [
                    "0: Confused, descriptive summary, without theses.",
                    "1-2: Descriptive summary, rough.",
                    "3-4: Clear, segmented summary, correct.",
                    "5: Impeccable, down-to-earth, progressive designer."
                ]
            },
            {
                cim: "Overall impression, consultant/opponent's opinion",
                leiras: "",
                pontok: [
                    "0: The thesis is not professionally relevant and does not meet the content and form requirements.",
                    "1-2: The thesis is less professionally relevant and meets the content and form requirements.",
                    "3-4: The thesis reflects correct professional and methodological preparation.",
                    "5: The thesis reflects excellent professional preparation."
                ]
            }
        ]
    }
};

function setLanguage(newLang) {
    lang = newLang;
    var t = i18n[lang];

    document.title = t.pageTitle;
    document.documentElement.lang = lang;

    var el = function(id) { return document.getElementById(id); };

    el("header-cim").textContent = t.headerCim;
    el("szerepkor").options[0].text = t.szerepkorKonzulens;
    el("szerepkor").options[1].text = t.szerepkorOpponens;
    el("hallgato-adatai-cim").textContent = t.hallgatoAdatai;
    el("nev-label").textContent = t.hallgatoNeve;
    el("nev").placeholder = lang === "hu" ? "Pl. Kovács János" : "e.g. John Smith";
    el("kod-label").textContent = t.hallgatoNeptun;
    el("kod").placeholder = lang === "hu" ? "6 karakter (pl. AB1234)" : "6 characters (e.g. AB1234)";
    el("szak-label").textContent = t.hallgatoSzak;
    el("szak").options[0].text = t.szakProgramtervezo;
    el("szak").options[1].text = t.szakGazdasagi;
    el("cim-label").textContent = t.hallgatoCim;
    el("cim").placeholder = lang === "hu" ? "A szakdolgozat teljes címe" : "Full title of the thesis";
    el("ertekelesek-cim").textContent = t.ertekelesekCim;
    el("szoveges-ertekeles-cim").textContent = t.szovegesErtekelesCim;
    el("rovidErtekeles").placeholder = t.szovegesErtekelesPlaceholder;
    el("biraloi-javaslat-cim").textContent = t.biraloiJavaslatkCim;
    el("bitraloiJavaslat").placeholder = t.biraloiJavaslatkPlaceholder;
    el("kerdesek-cim").textContent = t.kerdesekCim;
    el("uj-kerdes-gomb").textContent = t.ujKerdesGomb;
    el("ossz-label").textContent = t.osszpontszam;
    el("javaslat-label").textContent = t.javasoltErdemjegy;

    for (var i = 1; i <= 10; i++) {
        var c = t.criteria[i - 1];
        el("criterion-cim-" + i).textContent = c.cim;
        if (el("criterion-leiras-" + i)) {
            el("criterion-leiras-" + i).textContent = c.leiras;
        }
        var ul = el("criterion-pontok-" + i);
        if (ul) {
            var items = ul.querySelectorAll("li");
            for (var j = 0; j < items.length && j < c.pontok.length; j++) {
                items[j].textContent = c.pontok[j];
            }
        }
        var textarea = el("megjegyzes" + i);
        if (textarea) textarea.placeholder = t.megjegyzesPlaceholder;
        var pontKijelzo = el("pont-kijelzo-label-" + i);
        if (pontKijelzo) pontKijelzo.textContent = t.adottPont;
    }

    updateScore();
}

const BACKEND_URL = "http://localhost:5039";

function sendData(endpoint, filename) {
    var data = getAllData();
    if (data === null) return;

    fetch(BACKEND_URL + endpoint, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    })
    .then(function(response) {
        if (!response.ok) {
            throw new Error(i18n[lang].hibaSzerver + response.status);
        }
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
        alert(i18n[lang].hibaLetoltes + err.message);
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
    setLanguage("hu");
};
