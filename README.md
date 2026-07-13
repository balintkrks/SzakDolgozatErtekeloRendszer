# Szakdolgozat Bírálat Kiértékelő Rendszer

A **Szakdolgozat Bírálat Kiértékelő Rendszer** egy webes alkalmazás, amely megkönnyíti az egyetemi oktatók számára a szakdolgozatok bírálati lapjainak kitöltését. A rendszer egy egyszerű, reszponzív webes felületet biztosít, ahol a bírálat gyorsan és kényelmesen elkészíthető, majd különböző dokumentumformátumokban exportálható.

## Technológiák

### Frontend
- HTML5
- CSS3
- Bootstrap
- JavaScript

### Backend
- ASP.NET Core (.NET 8)
- C#

### Dokumentumgenerálás
- PDF – QuestPDF
- Microsoft Word – Open XML SDK / DocX
- LaTeX – `.tex` fájl generálása sablon alapján

### Fejlesztői környezet
- Visual Studio 2022
- Visual Studio Code

---

## Főbb funkciók

- Hallgatói adatok rögzítése
  - Név
  - Neptun-kód
  - Szak
  - Szakdolgozat címe

- Értékelési szempontok kitöltése
  - Pontszámok megadása
  - Opcionális szöveges indoklások

- Automatikus számítások
  - Összpontszám kiszámítása
  - Javasolt érdemjegy automatikus meghatározása

- További értékelési elemek
  - Szöveges értékelés
  - Javaslatok a védéshez
  - A hallgató számára megfogalmazott kérdések dinamikus hozzáadása

- Bírálói szerepkör kiválasztása
  - Konzulens
  - Opponens

- Dokumentum export
  - PDF
  - Microsoft Word (.docx)
  - LaTeX (.tex)
  - Zip

---

## Működés

1. A felhasználó megadja a hallgató adatait.
2. Kitölti az értékelési szempontok pontszámait, szükség esetén indoklással együtt.
3. Opcionálisan megadhat szöveges értékelést és védési javaslatokat.
4. Felveheti a hallgatónak szánt kérdéseket, amelyek dinamikusan bővíthetők.
5. Kiválasztja a bíráló szerepkörét (konzulens vagy opponens).
6. A rendszer folyamatosan ellenőrzi a kötelező mezők kitöltését és az adatok helyességét.
7. Automatikusan kiszámítja az összpontszámot és a javasolt érdemjegyet.
8. A kiválasztott formátumban elkészíti és letölti a bírálati dokumentumot.
