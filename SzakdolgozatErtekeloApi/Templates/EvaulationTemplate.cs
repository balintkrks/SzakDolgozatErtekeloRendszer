using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzakdogaBiralatTeszt
{
    public class EvaulationTemplate
    {
        public static List<EvaluationCriterion> GetDefaultCriteria()
        {
            return new List<EvaluationCriterion>
            {
                new EvaluationCriterion
                {
                    ID = 1,
                    Title = "1. A szakdolgozat szerkezeti felépítése, tartalmi tagolása, alaki megjelenése.",
                    ZeroPointText = "A dolgozat nem felel meg az EKKE szakdolgozati követelményeinek.",
                    OneTwoPointText = "A követelmények-nek lényegében megfelel, de nagyobb hiányosságokkal.",
                    ThreeFourPointText = "A követelmé-nyeknek lényegében megfelel, kisebb hiányosságokkal.",
                    FivePointText = "Kifogástalan szerkezet, tartalmi tagolás, szép kivitel.",
                   
                },

                new EvaluationCriterion
                {
                    ID = 2,
                    Title = "2. A szakdolgozat nyelvezete, stílusa, nyelvi helyessége.",
                    ZeroPointText = "Nyelvezete mondatszerkesztése erősen kifogásolható, durva helyesírási hibákat tartalmaz.",
                    OneTwoPointText = "Nyelvezet, stílusa sok hiányossággal, kisebb helyesírási hibákat tartalmaz.",
                    ThreeFourPointText = "Nyelvezete megfelelő, kevés stílushibával.",
                    FivePointText = "Kifogástalan nyelvezet és stílus.",
                    
                },

                new EvaluationCriterion
                {
                    ID = 3,
                    Title = "3. A dolgozat szakirodalmi hátterének feltárása, szakszerűsége, a hivatkozások helyessége.",
                    ZeroPointText = "A szakirodalom nem releváns. A hivatkozások hiányoznak, vagy nem szakszerűek. ",
                    OneTwoPointText = "A szakirodalom feltárása szűk körű, csak tankönyvek, vagy jegyzetek anyagát tartalmazza. Hivatkozásai több helyen pontatlanok.",
                    ThreeFourPointText = "Legfontosabb szakirodalmakat, korrekt hivatkozásokat tartalmaz a dolgozat. Az elmélet koherens, egységet alkot. ",
                    FivePointText = "Legújabb széles körű szakirodalom alapján íródott a dolgozat. Hivatkozások pontosak.",
                    
                },

                new EvaluationCriterion
                {
                    ID = 4,
                    Title = "4. A megvalósított feladat (program) nehézsége, összetettsége.",
                    ZeroPointText = "A megvalósított feladat nem teljesíti a szakdolgozattól elvárt nehézségi szintet.",
                    OneTwoPointText = "A megvalósított feladat éppen teljesíti a szakdolgozattól elvárt nehézségi szintet. ",
                    ThreeFourPointText = "A megvalósított feladat teljesíti a szakdolgozattól elvárt nehézségi szintet.",
                    FivePointText = "A megvalósított feladat túlmutat a szakdolgozattól elvárt nehézségi szinten.",
                    
                },

                new EvaluationCriterion
                {
                    ID = 5,
                    Title = "5. Megvalósítás minősége I.A feladat ismertetése, a célkitűzés definiálása (specifikáció, megvalósítási terv, összefüggések elemzése).",
                    ZeroPointText = "A dolgozatból a feladat ismertetése, illetve a célkitűzés definiálása teljes mértékben hiányzik.",
                    OneTwoPointText = "A dolgozatban megjelenik a feladat ismertetése, továbbá a célkitűzés definiálása, de a leírás alapszintű, elnagyolt, hiányos.",
                    ThreeFourPointText = "A dolgozatban a feladat ismertetése, illetve a célkitűzés definiálása formailag megfelelő, egy alapszintű leíráson túlmutat, szakmailag megfelelő, de nem teljes és ellentmondásmentes.",
                    FivePointText = "A dolgozatban  a feladat ismertetése, illetve a célkitűzések definiálása szakmailag korrekt, formailag megfelelő, teljes és ellentmondásmentes.",
                    
                },

                new EvaluationCriterion
                {
                    ID = 6,
                    Title = "6. Megvalósítás minősége II.Kódminőség.",
                    ZeroPointText = "A forráskód tartalma, mennyisége, szerkezete nem megfelelő.",
                    OneTwoPointText = "A forráskód tartalma, mennyisége, szerkezete megfelelő, de nehézen áttekinthető, terjengős, strukturálatlan, és helyenként indokolatlan nyelvi elemeket tartalmaz.Nem tartalmazza a szükséges ellenőrzési, hibakezelési funkciókat.",
                    ThreeFourPointText = "A forráskód tartalma, mennyisége, összetettsége jó. A forráskódban megjelennek a korszerű, hatékony nyelvi elemek. Jól strukturált, áttekinthető, tartalmazza a szükséges ellenőrzési, hibakezelési funkciókat.",
                    FivePointText = "A forráskód tartalma, mennyisége, összetettsége kiváló. Korszerű, hatékony nyelvi elemeket tartalmaz. Jól strukturált, áttekinthető, öndokumentáló, újrafelhasználható. Megvalósított a teljeskörű hiba- és kivételkezelés. Erőforrás-ütemezés szempontjából optimalizált.",
                    
                },

                new EvaluationCriterion
                {
                    ID = 7,
                    Title = "7. Megvalósítás minősége III.Fejlesztési- és felhasználói dokumentáció.",
                    ZeroPointText = "A Fejlesztési- és felhasználói dokumentáció nem felel meg a követelményeknek. ",
                    OneTwoPointText = "A Fejlesztési- és felhasználói dokumentáció elnagyolt, hiányos, szerkezete logikátlan.",
                    ThreeFourPointText = "A Fejlesztési- és felhasználói dokumentáció tartalmazza a fontosabb fejezeteket, szerkezete logikus, a fejezetek közötti kohézió jó.",
                    FivePointText = "A Fejlesztési- és felhasználói dokumentáció kiváló színvonalú, a dokumentálási szabályoknak teljes mértékben megfelel.",
                   
                },

                new EvaluationCriterion
                {
                    ID = 8,
                    Title = "8. Megvalósítás minősége IV.Tesztelés, futtatás.",
                    ZeroPointText = "A dolgozat nem tartalmaz teszteléshez köthető részeket. A szoftver nem futtatható és/vagy nem a terveknek megfelelően működik.",
                    OneTwoPointText = "A dolgozatban megjelennek a teszteléssel kapcsolatos törekvések, de elnagyoltak, hiányosak.",
                    ThreeFourPointText = "Jó színvonalú, de nem teljes körű tesztelés.",
                    FivePointText = "Teljes körű és jól dokumentált validáció és verifikáció (formális módszerek, tesztelési terv, tesztelési jegyzőkönyv és jelentés).",
                    
                },

                new EvaluationCriterion
                {
                    ID = 9,
                    Title = "9. A szakdolgozat összefoglalása.",
                    ZeroPointText = "Zavaros, leíró összefoglalás, tézisek nélkül.",
                    OneTwoPointText = "Leíró jellegű összefoglalás, elnagyolt.",
                    ThreeFourPointText = "Világos tagolt összefoglalás, korrekt.",
                    FivePointText = "Kifogástalan, lényegre törő, továbbtervező.",
                   
                },

                new EvaluationCriterion
                {
                    ID = 10,
                    Title = "10. Összbenyomás, konzulens/opponens véleménye",
                    ZeroPointText = "A dolgozat szakmailag nem releváns, tartalmi formai követelményeknek nem felel meg.",
                    OneTwoPointText = "A dolgozat szakmailag kevésbé releváns, tartalmi formai követelményeknek megfelel.",
                    ThreeFourPointText = "A dolgozat korrekt szakmai és módszertani felkészültséget tükröz.",
                    FivePointText = "A dolgozat kiváló szakmai felkészültséget tükröz.",
                    
                },

            };
        }
    }
}
