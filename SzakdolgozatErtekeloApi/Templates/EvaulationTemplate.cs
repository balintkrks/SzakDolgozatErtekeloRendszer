using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzakdolgozatErtekeloApi.Templates
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

        public static List<EvaluationCriterion> GetScientificCriteria()
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
                    Title = "4. A vizsgált probléma nehézsége, összetettsége.",
                    ZeroPointText = "A vizsgált nehézsége, összetettsége nem teljesíti a szakdolgozattól elvárt nehézségi szintet.",
                    OneTwoPointText = "A vizsgált probléma nehézsége, összetettsége éppen teljesíti a szakdolgozattól elvárt nehézségi szintet.",
                    ThreeFourPointText = "A vizsgált probléma nehézsége, összetettsége teljesíti a szakdolgozattól elvárt nehézségi szintet.",
                    FivePointText = "A vizsgált probléma nehézsége, összetettsége túlmutat a szakdolgozattól elvárt nehézségi szinten",

                },

                new EvaluationCriterion
                {
                    ID = 5,
                    Title = "5. A dolgozat újszerűsége, a célkitűzés definiálása (összefüggések elemzése).",
                    ZeroPointText = "A dolgozat az adott tématerületen létező, ismert eljárások megvalósítása, újszerű eredményeket nem tartalmaz.",
                    OneTwoPointText = "A dolgozat ismert eljárás(ok) megvalósítása, de újszerű eredményeket, összehasonlításokat is tartalmaz.",
                    ThreeFourPointText = "A dolgozat magas színvonalú. Az eredményeket ismert eljárások eredményével összeveti.",
                    FivePointText = "A dolgozat eredeti és újszerű elgondolást ismertet.",

                },

                new EvaluationCriterion
                {
                    ID = 6,
                    Title = "6. A dolgozat tartalmi megvalósítása, kidolgozottsága.",
                    ZeroPointText = "A dolgozat témája jól ismert, a tartalma elmélyült tudást nem igényelt.",
                    OneTwoPointText = "A dolgozat témája korszerű, de jól ismert, a szakirodalomban jól kidolgozott, a munka jórészt reproduktív jellegű",
                    ThreeFourPointText = "A dolgozat témája korszerű, de jól ismert, a szakirodalomban jól kidolgozott, azonban a vizsgálata alapos, elmélyült tudást, illetve széleskörű ismeretet igényelt.",
                    FivePointText = "A dolgozat témája korszerű, vizsgálata elmélyült, magas szintű tudást igényelt.",

                },

                new EvaluationCriterion
                {
                    ID = 7,
                    Title = "7. Az eredmények igazoltsága.",
                    ZeroPointText = "Az eredmények igazolása nem valósult meg. ",
                    OneTwoPointText = "Az eredmények igazoltsága nem meggyőző.",
                    ThreeFourPointText = "Az eredmények igazolásához további vizsgálatok lehetnek szükségesek.",
                    FivePointText = "Az eredmények matematikailag bizonyítottak, statisztikailag vagy reprezentatív mintán ellenőrzöttek, vagy más módon kielégítően igazoltak/validáltak.",

                },

                new EvaluationCriterion
                {
                    ID = 8,
                    Title = "8. Az elért eredmények elméleti/tudományos értéke.",
                    ZeroPointText = "A dolgozat nem képvisel ilyen jellegű értéket.",
                    OneTwoPointText = "A dolgozat további kutatómunka révén használható eredmény felmutatására lenne alkalmas.",
                    ThreeFourPointText = "A dolgozat eredménye, innovációs értéke jó.",
                    FivePointText = "A dolgozat eredménye, innovációs értéke nemzetközi jelentőséggel bír. Az eredmény kiváló.",

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

        public static List<EvaluationCriterion> GetEnglishCriteria()
        {
            return new List<EvaluationCriterion>
            {
                new EvaluationCriterion
                {
                    ID = 1,
                    Title = "1. The thesis's structure, content division, and appearance.",
                    ZeroPointText = "The thesis does not meet the thesis requirements of EKKE.",
                    OneTwoPointText = "The requirements , but with major shortcomings.",
                    ThreeFourPointText = "It essentially meets the requirements, with minor deficiencies .",
                    FivePointText = "Impeccable structure, content division, beautiful design.",

                },

                new EvaluationCriterion
                {
                    ID = 2,
                    Title = "The language, style and linguistic correctness of the thesis.",
                    ZeroPointText = "The sentence structure of his language is highly objectionable, it contains gross spelling errors.",
                    OneTwoPointText = "Language and style with many shortcomings and minor spelling errors.",
                    ThreeFourPointText = "His language is appropriate, with few stylistic errors.",
                    FivePointText = "Impeccable language and style.",

                },

                new EvaluationCriterion
                {
                    ID = 3,
                    Title = "3. The exploration of the technical literature background of the thesis, its professionalism, and the correctness of the references.",
                    ZeroPointText = "The literature is not relevant. References are missing or not professional.",
                    OneTwoPointText = "The exploration of the literature is narrow, it only includes material from textbooks or notes. Your references are inaccurate in several places.",
                    ThreeFourPointText = "The thesis contains the most important literature and correct references . The theory is coherent and forms a unity.",
                    FivePointText = "The thesis was written based on the latest extensive literature. Links are accurate.",

                },

                new EvaluationCriterion
                {
                    ID = 4,
                    Title = "4. The difficulty and complexity of the implemented task (program).",
                    ZeroPointText = "The completed task does not meet the level of difficulty expected from the thesis.",
                    OneTwoPointText = "The completed task meets the level of difficulty expected from the thesis.",
                    ThreeFourPointText = "The completed task fulfills the level of difficulty expected from the thesis.",
                    FivePointText = "The completed task goes beyond the level of difficulty expected from a thesis.",

                },

                new EvaluationCriterion
                {
                    ID = 5,
                    Title = "5. Quality of implementation I.\r\nDescription of the task, definition of the objective (specification, implementation plan, analysis of correlations).",
                    ZeroPointText = "The description of the task and the definition of the objective are completely missing from the thesis.",
                    OneTwoPointText = "The thesis contains a description of the task and a definition of the objective, but the description is basic, rough, and incomplete.",
                    ThreeFourPointText = "The description of the task and the definition of the objective in the thesis are formally appropriate, go beyond a basic level description, professionally appropriate, but not complete and without contradictions.",
                    FivePointText = "The description of the task and the definition of the objectives in the thesis are professionally correct, formally appropriate , complete and without contradictions.",

                },

                new EvaluationCriterion
                {
                    ID = 6,
                    Title = "6. Quality of implementation II. Code quality.",
                    ZeroPointText = "The content, quantity, and structure of the source code are not appropriate .",
                    OneTwoPointText = "The content, amount, and structure of the source code is adequate, but it is difficult to understand, it is extensive, unstructured, and sometimes contains unjustified language elements. It does not include the necessary control and error management functions.",
                    ThreeFourPointText = "The content, quantity and complexity of the source code are good. Modern, efficient language elements appear in the source code. It is well structured, transparent, and contains the necessary control and error management functions.",
                    FivePointText = "The content, quantity and complexity of the source code are excellent. It contains modern, effective language elements. Well structured, clear, self-documenting, reusable . Complete error and exception handling has been implemented. Optimized for resource scheduling.",

                },

                new EvaluationCriterion
                {
                    ID = 7,
                    Title = "7. Quality of implementation III. Development and user documentation.",
                    ZeroPointText = "The Development and User Documentation does not meet the requirements.",
                    OneTwoPointText = "The Development and user documentation is rough, incomplete, and its structure is illogical.",
                    ThreeFourPointText = "The Development and User Documentation contains the most important chapters, its structure is logical, and the cohesion between the chapters is good.",
                    FivePointText = "The Development and user documentation is of excellent quality and fully complies with the documentation rules.",

                },

                new EvaluationCriterion
                {
                    ID = 8,
                    Title = "8. Quality of implementation IV. Testing, running.",
                    ZeroPointText = "The thesis does not contain parts related to testing. The software does not run and/or does not work as intended.",
                    OneTwoPointText = "Attempts related to testing appear in the thesis, but they are rough and incomplete.",
                    ThreeFourPointText = "Good quality, but not comprehensive testing.",
                    FivePointText = "Comprehensive and well-documented validation and verification (formal methods, test plan, test protocol and report).",

                },

                new EvaluationCriterion
                {
                    ID = 9,
                    Title = "9. Summary of the thesis.",
                    ZeroPointText = "Confused, descriptive summary, without theses.",
                    OneTwoPointText = "Descriptive summary, rough.",
                    ThreeFourPointText = "Clear, segmented summary, correct.",
                    FivePointText = "Impeccable, down-to-earth, progressive designer .",

                },

                new EvaluationCriterion
                {
                    ID = 10,
                    Title = "10. Overall impression, consultant/opponent's opinion.",
                    ZeroPointText = "The thesis is not professionally relevant and does not meet the content and form requirements.",
                    OneTwoPointText = "The thesis is less professionally relevant and meets the content and form requirements.",
                    ThreeFourPointText = "The thesis reflects correct professional and methodological preparation.",
                    FivePointText = "The thesis reflects excellent professional preparation.",

                },

            };
        }
    }
}
