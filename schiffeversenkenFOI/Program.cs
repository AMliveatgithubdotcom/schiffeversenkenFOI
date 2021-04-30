using System.Collections.Generic;
using System.Linq;
using System;

/*
_______________________________ 
|#|#|#|_|_|_|_|_|_|_|_|_|_|_|_| # = Schifffeld
|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_| X = Treffer
|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_| O = Daneben
|_|_|_|_|_|X|_|X|_|_|_|_|_|_|_| _ = Leer
|_|_|_|_|_|_|X|_|_|_|_|_|_|_|_|
|_|_|_|_|x|X|O|O|O|X|_|_|_|_|_|
|_|_|_|_|_|_|_|_|X|_|_|_|_|_|_|
|_|_|_|_|_|_|_|X|X|_|_|_|_|_|_|
|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|
|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|
|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|
|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|
 */

namespace schiffeversenkenFOI
{

    class MainClass
    {
        static void Main()
        {
            ///////////////////////////////////////////////////
            ///         Startbildschirm
            //////////////////////////////////////////////////
            
            /*              Startbildschirm Abbildung eines Schlachtschiffes in ASCII-Art
                                          |
                                         \|/                        */
            Console.Write("\r\n   .  .    .  .    .    ..S:8 .;8    .    .  .    .  . .  .  . . ...:      . .  \r\n  .    .  .    .  .  . . t    .t8   .  .   .   .  ...      ...    t SS : .     .\r\n    .       .         ..:S:@ . 8X. .     .. . ... S8t%8 8 . tS% ;%S 88... . .   \r\n  .   . .    .  .  .  . X S .. X.8..  . .:8X:8 :8S X 8.S.X.8@:X:@..t8 SX.X8X@.. \r\n    .     .    .    .. 8;X;. : S  :.. . X;..  .S8 :  X.  888 . 8: @   S.. .   8 \r\n  .    .   .     .   .. ;S . ..t8:%X..  ttt8X8;8... .:% 8.@X.8;@; %.8%.....8;;;8\r\n     .   .   .    ...: :tt.    ..88tt;..8:X .. .. ;tS@:.S @;%S...  . . .   .:@8 \r\n  .    .      . .   ;@ .: . .   ..X:.   t.8....  tX  ;8@    :..          .. ..@ \r\n    .     . .   . XXSX.  .    .  . 888S t 8..%;S@S;X%;. .          . .  . X8S8S \r\n  .   .        . ..  ..    .        ..X%% 8 @S.S8.: .     .   .  .    . .%S;%.. \r\n    .   . .  . . 8.t..   .   . .  ..  8..  8:.. ..  .  .   .       . . 8S 8S .  \r\n  .         .  .. :@ .     .  . ..S.  @8 .8.  .      .   .   . .    ..;S  X..  .\r\n     . .  .   . ;X8 . ... .. t@%;@.8S.X. ..     .  .   .        . ..: .:8..  .  \r\n  .           . @ X...   %.@S   %S8  ..     . .      .    . .     ..X. ...      \r\n    .  . ... ...X 8;%@ X@@; 8:S8:. ..     .      . .    .     .  ..  8..   .  . \r\n  .  . . t8t%X %;8% 8;XX:.:  ...  .    .    .  .      .   .  .  . t.:S . .   .  \r\n   . @  S   .@ ;X    ....  .    .    .   .       .  .   .      ..8::       .    \r\n   . : @@;;8;    .            .   .    .   . .        .    .  ..  :8S . .     . \r\n...:.% ..::... ...............  .  . .  ... .......... ... ... ..t.%.... .......\r\n      .%   .                  %S%SS%SSSS%tttt;ttttttttttt8.   . S               \r\n88888;:88888888888888888888888............................;88888S.@8888888888888\r\nt;;;;:tt;t;;;;;;;t;;t;;;;;;;t;;t;tt;t;t;ttttttttttttttttt;;tt;;tttt;;;;;t;;;;;;t\r\n......... ....... ............... ....... .. .. .. .. .. ... .. .. ..... .......\r\n                 .                                             .                \r\n  .   .  .  .  .      .  .     .  . Press Space  .   .  .  . .    .   . . .  .  ");
            
            Console.ReadKey();                      //Das Spiel soll erst anfangen wenn irgend eine Taste gedrückt worden ist.

            //////////////////////////////////////////////////
            ///         Initialisierung der Felder
            //////////////////////////////////////////////////
            Field player = new Field();                             //Das Feld des Spielers wird erstellt
            Field ki = new Field(true);                             //Das Feld der KI wird erstellt
            Field shadow = ki.Clone_to_field_visible_to_player();   //Das Feld, welches der Spieler sehen darf, um seine Treffer zu sehen ist. Das Feld ist eine Art Kopie des KI Feldes. Dieses Feld trägt den Namen shadow(Schatten), weil ich es passend fand. Dieses Feld ist nicht identisch wie eine Kopie sondern ungenauer wie ein Schatten.

            //////////////////////////////////////////////////
            ///         Spielfelder erstellen
            //////////////////////////////////////////////////

            //////////////////////////////////////////////////
            ///         Spiel starten
            //////////////////////////////////////////////////

            Field.DrawUI_inGame(player, shadow);                    //Das Spiel Wird aus den Spielfeldern des Spielers und des Schattens gezeichnet
            Console.ReadKey();                                      //Es wird gewartet bis eine Taste gezeichnet wird

        }

        class Field
        {
            private char[,] casket = new char[10,10];                   //Die einzelnen Kästchen sind als array casket gespeichert(casket ist eng. und bedeutet kästchen). Es ist ein Char-Array da alle Kästchen ein Zeichen enthalten werden
            private readonly bool ki;                                   //Dieser bool speichert ob dieses Feld eine KI ist oder nicht
            private readonly List<string> used = new List<string>();    //Dies ist eine Liste, welche alle versuche speichert.

            public Field(bool is_KI = false)                            //hier wird der Wert den variablen casket und ki zugewiesen, falls kein Wert übergeben wird, an den Konstruktor, ist der Status der KI false also ausgeschaltet
            {
                ki = is_KI;                                             //zuweisung der Eingabe des Konstruktors in die Boolische variable
                for (byte spalte = 0; spalte < 10; spalte++)            //Die Schleife geht alle spalten durch und spechert die Zahl
                {
                    for (byte zeile = 0; zeile < 10; zeile++)           //Die Schleife geht alle Zeilen durch und spechert die Zahl
                    {
                        casket[spalte, zeile] = '_';                    //Die gespeicherten Zahlen in casket eingesetzt um das Feld als leer zu markieren, mehr dazu oben in der Skizze
                    }
                }
            }

            public char Get_casket(byte spalte, byte zeile)             //casket ist privat eingestellt, deshalb um die werte der einzelnen Kästchen zu bekommen werden hier die indexe eingegeben als spalte und zeile und der Wert and dieser Stelle, im Array, wird zurückgegeben
            {
                return casket[spalte, zeile];                           //Gibt den Wert an der Stelle von spalte, zeile zurück
            }

            public bool Make_a_move(int spalte, int zeile)              //Fier wird ein Zug gemacht. Die KI braucht keine Angaben, nur der Spieler die Koordinaten als 2 Integer. Die Methode gibt einen bool zurück true heist kein Fehler und false ein Fehler 
            {
                if (ki)                                                 //hier wird die Variable ki genutzt, um die Züge die diese Macht zu automatisieren. Der Teil in der If-clause wird nur ausgeführt wenn das Feld der Ki gehört
                {
                    Random generator = new Random();                    //Ein Random Objekt wird erstellt, um zufallszahlen für die KI zu generieren
                    do                                                  //mindestens einmal werden die variablen Spalte und Generator einen Wert zugewiesen bekommen
                    {
                        spalte = generator.Next(0, 11);                 //zuweisung einer Zahl von 1-10, aufgrund der Spielfeldgröße
                        zeile = generator.Next(0, 11);                  //zuweisung einer Zahl von 1-10, aufgrund der Spielfeldgröße
                    } while (used.Contains(spalte.ToString() + zeile.ToString()));  //hier wird geprüft, ob die Koordinate genutz worden ist.
                    used.Add(spalte.ToString() + zeile.ToString());                 //Die Koordinate wird als string gespeichert, da das den Code vereinfacht und keine Fehler entstehen (zeile = 7, spalte = 3 zusammen = 10 oder zeile = 3, spalte = 7 zusammen = 10)
                }

                switch (casket[spalte, zeile])                      //Diese Stelle wird unabhängig ob man eine KI oder ein Spieler ist ausgeführt im switch-case wird im Feld die Koordinate and der Stelle spalte Zeile geändert, mehr dazu oben in der Skizze
                {
                    case '_':                                       //falls der case das Zeichen '_' annimmt 
                        casket[spalte, zeile] = 'O';                //wird das Zeichen zu 'O' geändert, da ['_'= leer] und ['_'= verfehlt] bedeutet
                        break;
                    case '#':                                       //falls der case das Zeichen '_' annimmt
                        casket[spalte, zeile] = 'X';                //wird das Zeichen zu 'X' geändert, da ['X'= getroffen] und ['#'= Schiff] bedeutet
                        break;

                    default:
                        return false;                               //Im falle das der Spieler eine Koordinate eingibt die genutzt worden ist, wird false für fehler zurückgegeben
                }
                return true;                                        //Im normalfall wird true zurückgegeben, für keine Fehler
            }

            public Field Clone_to_field_visible_to_player()         //Hier wird ein Feld zurückgegeben anstatt mit Schiffen('#'), mit leeren Feldern('_')
            {
                char[,] alternativ = new char[10, 10];              //hier wird ein Leeres Feld erstellt die Alternative (Kopie)
                Field visible_to_player = new Field();              //Ein Objekt wird estellt der Klasse Feld

                for (byte spalte = 0; spalte < 10; spalte++)
                {
                    for (byte zeile = 0; zeile < 10; zeile++)
                    {
                        if (casket[spalte, zeile] == '#')
                        {
                            alternativ[spalte, zeile] = '_';        //alle Felder werden eingesetzt und im falle von '#' durch '_' ersetzt
                        }
                        else
                        {
                            alternativ[spalte, zeile] = casket[spalte, zeile];
                        }
                    }
                }
                visible_to_player.casket = alternativ;          //Die caskets in Felder werden durch alternativ ersetzt
                return visible_to_player;                       //Das Objekt visible_to_player wird zurückgegeben
            }

            public static void DrawUI_inGame(Field playerField, Field visible_to_player)//Die Methode nimmt zwei Felder und bildet diese in der Konsole ab
            {
                Console.Clear();
                Console.WriteLine("  A B C D E F G H I J\t\t  A B C D E F G H I J");//Hier wird ein Teil der Benennung der Koordinaten gezeichnet
                for (byte spalte = 0; spalte < 10; spalte++)                //Die Schleife 1 speichert in "spalte" eine zahl von 1 bis 10
                {                                                           
                    Console.Write(spalte);
                    for (byte zeile = 0; zeile < 10; zeile++)              //Die Schleife 2 speichert in "zeile" eine zahl von 1 bis 10, dieser vorgang wird 10 wiederholt innerhalb Schleife 1. In Schleife 2 wird das Spieler Feld gezeichnet
                    {
                        Console.Write("|" + playerField.Get_casket(spalte, zeile));         //In Schleife 2 wird im playerfield Array die Zeile und die Spalte der Ausgewählten zelle abgebildet hinter einem pipe zeichen
                    }

                    Console.Write("|\t\t" + spalte);                                 //Da die Felder Neben einander Sind die der Ki und das des Spielers werden die Felder mit tabs getrennt

                    for (byte zeile = 0; zeile < 10; zeile++)              //Die Scleife 3 speichert in "spalte" eine zahl von 1 bis 10, dieser vorgang wird 10 wiederholt innerhalb Schleife 1. In Schleife 2 wird das KI Feld gezeichnet
                    {
                        Console.Write("|" + visible_to_player.Get_casket(spalte, zeile));             //In Schleife 3 wird im kiField Array die Zeile und die Spalte der Ausgewählten zelle abgebildet hinter einem pipe zeichen
                    }
                    Console.Write("|\n");                                   //die Nächste Zeile wird gezeichnet
                }
            }
        }
    }
}


