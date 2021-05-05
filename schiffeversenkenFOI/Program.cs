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
            //////////////////////////////////////////////////
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
            Field player = new Field(true);                         //Das Feld des Spielers wird erstellt, KI wird auf true gesetzt, da der gegenspieler, die KI, so aktiviert wird
            Field ki = new Field();                                 //Das Feld der KI wird erstellt

            //////////////////////////////////////////////////
            ///         Spielfelder editieren
            //////////////////////////////////////////////////
            BuildingPhase(player, 5);                               //hier werden die verschiedenen Schiffe plaziert mit den Angaben des Spielers, diese werden in den Funktionen abgefragt
            BuildingPhase(player, 4);
            BuildingPhase(player, 3);
            BuildingPhase(player, 2);
            BuildingPhase(player, 2);

            BuildingPhase_KI(ki, 5);                                //hier werden die verschiedenen Schiffe plaziert, nach zufall der KI
            BuildingPhase_KI(ki, 4);
            BuildingPhase_KI(ki, 3);
            BuildingPhase_KI(ki, 2);
            BuildingPhase_KI(ki, 2);

            Field shadow = ki.Clone_to_field_visible_to_player();   //Das Feld, welches der Spieler sehen darf, um seine Treffer zu sehen ist. Das Feld ist eine Art Kopie des KI Feldes. Dieses Feld trägt den Namen shadow(Schatten), weil ich es passend fand. Dieses Feld ist nicht identisch wie eine Kopie sondern ungenauer wie ein Schatten.

            //////////////////////////////////////////////////
            ///         Spiel starten
            //////////////////////////////////////////////////

            Field.DrawUI_inGame(player, ki);                    //Das Spiel Wird aus den Spielfeldern des Spielers und des Schattens gezeichnet
            string eingabe;                                         //Der String eingabe wird deklariert

            do {
                do                                                      //in der Schleife wird wiederholt, bis die Eingabe passend ist
                {
                    Console.Write("Coordinats: ");
                    eingabe = Console.ReadLine();                       //Der string wird deklariert

                } while (ki.Make_a_move(Convert.ToInt32(eingabe[0]) - 97, Convert.ToInt32(eingabe[1]) - 48));   //hier wird die eingabe getestet + die Bedingung wird hier ausgeführt
                player.Make_a_move(0, 0);                               //beim Spieler wird automatisch gespielt, die Eingabe ist also egal
                shadow = ki.Clone_to_field_visible_to_player();
                Field.DrawUI_inGame(player, ki);                        //hier kommt shadow hin
            } while (!(player.Has_lost() | ki.Has_lost()));
            
            Console.Clear();
            if (player.Has_lost())
            {
                //ascii-art wenn der Spieler verliert
                Console.WriteLine("\n\n\n\n\n\n\n |  /_ _| \\ \\        /           \r\n ' /   |   \\ \\  \\   / _ \\  __ \\  \r\n . \\   |    \\ \\  \\ / (   | |   | \r\n_|\\_\\___|    \\_/\\_/ \\___/ _|  _| \r\n                                 ");
            }
            else
            {
                //ascii-art wenn die KI verliert
                Console.WriteLine("  _ \\  |                       \\ \\        /           \r\n |   | |  _` | |   |  _ \\  __|  \\ \\  \\   / _ \\  __ \\  \r\n ___/  | (   | |   |  __/ |      \\ \\  \\ / (   | |   | \r\n_|    _|\\__,_|\\__, |\\___|_|       \\_/\\_/ \\___/ _|  _| \r\n              ____/                                   ");
            }
            Console.ReadKey();                                      //Es wird gewartet bis eine Taste gezeichnet wird
        }

        static void BuildingPhase_KI(Field of_ki, byte ship_size)
        {
            Random generator = new Random();
            bool horizontal;
            string koordinate;
            do {
                horizontal = Convert.ToBoolean(generator.Next(0, 1));
                koordinate = ((char)generator.Next(97, 106)).ToString() + generator.Next(0, 10);
            } while (of_ki.Place_ship(ship_size, horizontal, koordinate));
        }

        static void BuildingPhase(Field player, byte ship_size)                 //Um code nicht zu wiederholen wird nur die Größe des Schiffes und das Feld des besitzers weitergegeben
        {
            player.Draw_field();                                    //hier wird nur das Feld des Spielers abgebildet, noch ist es leer
            Console.Write("\n\t\tPlacing the Battleship.\n[0]Horizontal -\n  or \n[1]Vertical |\nAnswer please with 0 or 1:");  // der Spieler wird gefragt, ob das Schiff horizontal oder Vertikal plaziert werden soll
            bool horizontal = Convert.ToBoolean(Convert.ToInt16(Console.ReadLine()));               //Die Antwort, die nur 0 oder 1, betragen kann wird in einen bool umgewandelt. false = horizontal, true = vertikal
            string koordinaten;                                                                     //Der String koordinaten wird deklariert da diese in form von a5 oder g0 eingegeben werden
            do                                                                                      //Eine do-while-Schleife fängt hier an
            {
                player.Draw_field();                                                                    //Das Spieler feld wird wieder gezeichnet, um nicht von vorher die Frage sehen zu können
                if (!horizontal)                                                                        //hier wird ausgewählt welcher pfad ausgegeben werden soll aufgrund von der horizontalen oder vertikalen
                {
                    Console.Write("\n\t\tWhere schould be the Battleship placed?(give the coordinate as CharacterInteger example: a5\nDominat side ->#####):");
                }
                else
                {
                    Console.Write("\nWhere schould be the Battleship placed?(give the coordinate as CharacterInteger example: a5\nDominat side -> #\n\t\t#\n\t\t#\n\t\t#\n\t\t#):");
                }
                koordinaten = Console.ReadLine();                                                       //Die Koordinaten werden erst hier in der Schleife abgefragt, nach dem die Fragegestellt worden ist
            } while (player.Place_ship(ship_size, horizontal, koordinaten));                                  //hier wird das 5-er Schiff eingezeichnet nach den eingegebenen angaben. falls ein fehler passiert wird das spieler Feld neugezeichenet und die Frage erneut gestellt bis diese richtig beantwortet ist.

        }

        class Field
        {
            private char[,] casket = new char[10,10];                   //Die einzelnen Kästchen sind als array casket gespeichert(casket ist eng. und bedeutet kästchen). Es ist ein Char-Array da alle Kästchen ein Zeichen enthalten werden
            private readonly bool ki;                                   //Dieser bool speichert ob dieses Feld eine KI ist oder nicht
            private readonly List<string> possebilieties;               //Dies ist eine Liste, welche alle versuche speichert.
            private byte winning_points = 16;

            public Field(bool is_player = false)                            //hier wird der Wert den variablen casket und ki zugewiesen, falls kein Wert übergeben wird, an den Konstruktor, ist der Status der KI false also ausgeschaltet
            {
                ki = is_player;                                             //zuweisung der Eingabe des Konstruktors in die Boolische variable
                for (byte spalte = 0; spalte < 10; spalte++)            //Die Schleife geht alle spalten durch und spechert die Zahl
                {
                    for (byte zeile = 0; zeile < 10; zeile++)           //Die Schleife geht alle Zeilen durch und spechert die Zahl
                    {
                        casket[spalte, zeile] = '_';                    //Die gespeicherten Zahlen in casket eingesetzt um das Feld als leer zu markieren, mehr dazu oben in der Skizze
                    }
                }
                if (ki)                                                 //Falls KI wahr ist werden hier in possebilities die möglichekeiten aller Züge gespeichert
                {
                    possebilieties = new List<string>() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99" };
                }
            }

            public char Get_casket(byte spalte, byte zeile)             //casket ist privat eingestellt, deshalb um die werte der einzelnen Kästchen zu bekommen werden hier die indexe eingegeben als spalte und zeile und der Wert and dieser Stelle, im Array, wird zurückgegeben
            {
                return casket[spalte, zeile];                           //Gibt den Wert an der Stelle von spalte, zeile zurück
            }

            public bool Make_a_move(int spalte, int zeile)              //Fier wird ein Zug gemacht. Die KI braucht keine Angaben, nur der Spieler die Koordinaten als 2 Integer. Die Methode gibt einen bool zurück true heist kein Fehler und false ein Fehler 
            {
                if (ki)                                                 //hier wird die Variable player genutzt, um die Züge der KI zu automatisieren. Der Teil in der If-clause wird nur ausgeführt wenn das Feld dem Spieler gehört
                {
                    Random generator = new Random();                    //Ein Random Objekt wird erstellt, um zufallszahlen für die KI zu generieren
                    string tmp = possebilieties[generator.Next(0, possebilieties.Count)];   //Die KI wählt von allen möglichen Feldern einen Zufälligen, welcher als String gespeichert ist
                    possebilieties.Remove(tmp);                         //Um dieselbe Wahl nichtmehr treffen zu können wird das ausgewählte element gelöscht aus der liste!
                    spalte = Convert.ToInt32(tmp[0] - 48);                   //Vom string wird der Index 0 in einen Integer umgewandelt und in der Variable spalte gespeichert
                    zeile = Convert.ToInt32(tmp[1]) - 48;                    //Vom string wird der Index 1 in einen Integer umgewandelt und in der Variable zeile  gespeichert
                }
                try
                {
                    switch (casket[zeile, spalte])                      //Diese Stelle wird unabhängig ob man eine KI oder ein Spieler ist ausgeführt im switch-case wird im Feld die Koordinate and der Stelle spalte Zeile geändert, mehr dazu oben in der Skizze
                    {
                        case '_':                                       //falls der case das Zeichen '_' annimmt 
                            casket[zeile, spalte] = 'O';                //wird das Zeichen zu 'O' geändert, da ['_'= leer] und ['_'= verfehlt] bedeutet
                            break;
                        case '#':                                       //falls der case das Zeichen '_' annimmt
                            casket[zeile, spalte] = 'X';                //wird das Zeichen zu 'X' geändert, da ['X'= getroffen] und ['#'= Schiff] bedeutet
                            winning_points -= 1;                        //falls ein Teil des Schiffes getroffen worden ist wird ein winning point abgezogen
                            break;

                        default:
                            return true;                               //Im falle das der Spieler eine Koordinate eingibt die genutzt worden ist, wird false für fehler zurückgegeben
                    }

                    return false;                                        //Im normalfall wird true zurückgegeben, für keine Fehler
                }
                catch (Exception)
                {

                    return true;
                }
            }

            public Field Clone_to_field_visible_to_player()             //Hier wird ein Feld zurückgegeben anstatt mit Schiffen('#'), mit leeren Feldern('_')
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

            public bool Has_lost()                                          //Hier wird zurückgegeben ob man verloren hat; true = verloren
            {
                if(winning_points == 0)                                 //Falls man 0 winningpoints hat hat man verloren, also wird hier der bool true zurückgegeben 
                {
                    return true;                                        //true = verloren
                }
                return false;                                           //false = noch nicht verloren
            }

            public static void DrawUI_inGame(Field playerField, Field visible_to_player)//Die Methode nimmt zwei Felder und bildet diese in der Konsole ab
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\t\t  A B C D E F G H I J\t\t  A B C D E F G H I J");//Hier wird ein Teil der Benennung der Koordinaten gezeichnet
                for (byte spalte = 0; spalte < 10; spalte++)                //Die Schleife 1 speichert in "spalte" eine zahl von 1 bis 10
                {                                                           
                    Console.Write("\t\t"+spalte);
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

            public void Draw_field()                                        //Hier wird einfach nur das Feld gezeichnet.
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\t\t  A B C D E F G H I J\t\t");//Hier wird ein Teil der Benennung der Koordinaten gezeichnet
                for (byte spalte = 0; spalte < 10; spalte++)                //Die Schleife 1 speichert in "spalte" eine zahl von 1 bis 10
                {
                    Console.Write("\t\t" + spalte);
                    for (byte zeile = 0; zeile < 10; zeile++)              //Die Schleife 2 speichert in "zeile" eine zahl von 1 bis 10, dieser vorgang wird 10 wiederholt innerhalb Schleife 1. In Schleife 2 wird das Spieler Feld gezeichnet
                    {
                        Console.Write("|" + casket[spalte, zeile]);         //In Schleife 2 wird im playerfield Array die Zeile und die Spalte der Ausgewählten zelle abgebildet hinter einem pipe zeichen
                    }
                    Console.WriteLine("|");
                }
            }
        
            public bool Place_ship(byte type, bool richtung, string koordinate) //Hier werden die Schiffe plaziert und ein bool zurückgegeben true = ein Fehler, false = alles Okay
            {
                char[,] backup = casket;                                        //das Feld wird in backup gespeichert
                byte spalte = (byte)(Convert.ToInt32(koordinate[0]) - 97);      //hier wird der Buchstabe einer Koordinate in ein byte umgewandelt, um so den index herauszufinden. a ist als int 97 allerdings soll dies index 0 bedeuten, also wird - 97 gerechenet. Die Zahl wird in einem byte gespeichert, da es weniger platz braucht
                byte zeile = (byte)(Convert.ToInt32(koordinate[1]) - 48);       //hier die Nummer einer Koordinate in ein byte umgewandelt, um so den index herauszufinden. 0 ist als int 48 allerdings soll dies index 0 bedeuten, also wird - 48 gerechenet. Die Zahl wird in einem byte gespeichert, da es weniger platz braucht

                if (!richtung)                                                  //hier wird darauf geachtet, ob das Schiff vertikal oder Horizontal ist
                {
                    if (spalte + type < 11)
                    {
                        for (byte extrapart = 0; extrapart < type; extrapart++)     //hier wird dei größe des Schiffes angepasst
                        {
                            if ((casket[zeile, spalte + extrapart] == '#'))           //falls das ausgewählte Feld schon besetzt ist passiert:
                            {
                                casket = backup;                                    //casket wird mit dem backup überschrieben
                                return true;                                        //und es wird ein Fehler zurückgegeben
                            }
                            casket[zeile, spalte + extrapart] = '#';                //Falls das Feld nicht schon beansprucht ist wird ein Schiffzeichen an dieser Stelle gesetzt
                        }
                    }
                    else
                    {
                        return true;                                            //Falls das Schiff nicht rein passt wird ein Fehler zurückgegeben
                    }
                }
                else                                                            //Im Else block wird der code ausgeführt welcher für die Vertikalen zuständig ist.
                {
                    if (zeile + type < 11)
                    {
                        for (byte extrapart = 0; extrapart < type; extrapart++)     //hier wird dei größe des Schiffes angepasst
                        {

                            if ((casket[zeile + extrapart, spalte] == '#'))           //falls das ausgewählte Feld schon besetzt ist passiert: 
                            {
                                casket = backup;                                    //casket wird mit dem backup überschrieben
                                return true;                                        //und es wird ein Fehler zurückgegeben
                            }
                            casket[zeile + extrapart, spalte] = '#';                //Falls das Feld nicht schon beansprucht ist wird ein Schiffzeichen an dieser Stelle gesetzt
                        }
                    }
                    else
                    {
                        return true;                                        //Falls das Schiff nicht rein passt wird ein Fehler zurückgegeben
                    }
                }
            return false;                                                   //Wenn alles bis hier durchgelaufen ist wird zurückgegeben, dass es keine Fehler gab
            }
        }
    }
}
