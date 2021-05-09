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
            //////////////////////////////////////////////////
            ///         Spiel starten
            //////////////////////////////////////////////////

            Field.DrawUI_inGame(player, ki.Clone_to_field_visible_to_player());                    // Erste Zeichnung der Spielfelder
            string eingabe;                                         //Deklaration string Eingabe, welches für die Eingabe zuständig ist.
            bool success = false;
            do
            {
                while (success == false)                            // Anfang einer While-Schleife mit try-catch statements, um schwerwiegende Fehler und Exceptions durch ungültigen Eingaben zu umgehen. 
                {
                    try
                    {
                        Console.Write("Coordinates: ");
                        eingabe = Console.ReadLine();                       //Holt input des Spielers
                        if (ki.Make_a_move(Convert.ToInt32(eingabe[0]) - 97, Convert.ToInt32(eingabe[1]) - 48)) success = true;     // Die Funktion Make_a_move beinhaltet weitere Prüfungen, um sicherzustellen, das die Eingabe gültig war.
                        else success = false;
                    }
                    catch { success = false; }
                 }
                player.Make_a_move(0, 0);                               //beim Spieler wird automatisch gespielt, die Eingabe ist also egal              
                Field.DrawUI_inGame(player, ki.Clone_to_field_visible_to_player()); //Zeichnet das Spielfeld, nachdem alle Eingaben für eine Runde durchgegangen sind.
                success = false;                                        //Bereitet wieder die Haupt While-Schleife vor.
            } 
            while (!(player.Has_lost() | ki.Has_lost()));               //Wiederholt alle abfragen der Eingabe falls noch kein Sieger festgestellt wurde.
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
            string koordinaten;                                                                        //Der String koordinaten wird deklariert da diese in form von a5 oder g0 eingegeben werden
            try                                                                                        // Error handling, falls jemand auf die Idee kommt etwas einzugeben, was nicht 0 oder 1 wäre.
            {
                byte choice = (Convert.ToByte(Console.ReadLine()));
                if (choice < 2)
                {
                    bool choicebool = Convert.ToBoolean(choice);                                                            //Konvertiert den Input 
                    do                                                                                      //Eine do-while-Schleife fängt hier an
                    {
                        player.Draw_field();                                                                    //Das Spieler feld wird wieder gezeichnet, um nicht von vorher die Frage sehen zu können
                        if (!choicebool)                                                                        //hier wird ausgewählt welcher pfad ausgegeben werden soll aufgrund von der horizontalen oder vertikalen
                            Console.Write("\n\t\tWhere should the Battleship be placed?\n(Give the coordinate in following format: Letter, Number. Example: a5)\nOrientation of ship:  ->#####):");
                        else
                            Console.Write("\nWhere should the Battleship be placed?\n(Give the coordinate in following format: Letter, Number. Example: a5)\nOrientation of ship: -> #\n\t\t#\n\t\t#\n\t\t#\n\t\t#):");
                        koordinaten = Console.ReadLine();                                                       //Die Koordinaten werden erst hier in der Schleife abgefragt, nach dem die Fragegestellt worden ist
                    } 
                    while (player.Place_ship(ship_size, choicebool, koordinaten));                                  //hier wird das 5-er Schiff eingezeichnet nach den eingegebenen angaben. falls ein fehler passiert wird das spieler Feld neugezeichenet und die Frage erneut gestellt bis diese richtig beantwortet ist.
                }
                else throw new DivideByZeroException();                                                          //Es gibt sicherlich bessere Wege, einen Fehler zu erzwingen um zum catch-statement zu wechseln, aber diese Methode ist lustiger und funktioniert auch gut.
            }
            catch
            {
                BuildingPhase(player, ship_size);
            }
    }

        class Field
        {
            private char[,] box = new char[10,10];                   //Die einzelnen Kästchen sind als array elemente box gespeichert. Wir benutzen einen Char-Array, da alle Kästchen nur ein Zeichen enthalten werden
            private readonly bool ki;                                   //Dieser bool speichert ob dieses Feld von der KI besetzt ist oder nicht
            private readonly List<string> possibilities = new List<string>();               //Dies ist eine Liste, welche alle versuche speichert.
            private byte winning_points = 16;

            public Field(bool is_player = false)                            //hier wird der Wert den variablen box und ki zugewiesen, falls kein Wert übergeben wird, an den Konstruktor, ist der Status der KI false also ausgeschaltet
            {
                ki = is_player;                                             //zuweisung der Eingabe des Konstruktors in die Boolische variable
                for (byte spalte = 0; spalte < 10; spalte++)            //Die Schleife geht alle spalten durch und spechert die Zahl
                {
                    for (byte zeile = 0; zeile < 10; zeile++)           //Die Schleife geht alle Zeilen durch und spechert die Zahl
                    {
                        box[spalte, zeile] = '_';                    //Die gespeicherten Zahlen in box eingesetzt um das Feld als leer zu markieren, mehr dazu oben in der Skizze
                    }
                }
                if (ki)                                                 //Falls KI wahr ist werden hier in possibilities alle möglichen Koordinaten gespeichert.
                {
                    for (int i = 0; i < 99; i++)
                    {
                        string newEntry = string.Format("{0:00}", i);
                        possibilities.Add(newEntry);
                    }
                    Console.WriteLine("sex");
                }
            }

            public char Get_box(byte spalte, byte zeile)              //box ist privat eingestellt, deshalb um die werte der einzelnen Kästchen zu bekommen werden hier die indexe eingegeben als spalte und zeile und der Wert and dieser Stelle, im Array, wird zurückgegeben
            {
                return box[spalte, zeile];                            //Gibt den Wert an der Stelle von spalte, zeile zurück
            }

            public bool Make_a_move(int spalte, int zeile)               //Fier wird ein Zug gemacht. Die KI braucht keine Angaben, nur der Spieler die Koordinaten als 2 Integer. Die Methode gibt einen bool zurück true heist kein Fehler und false ein Fehler 
            {
                if (ki)                                                  //hier wird die Variable player genutzt, um die Züge der KI zu automatisieren. Der Teil in der If-clause wird nur ausgeführt wenn das Feld dem Spieler gehört
                {
                    Random generator = new Random();                     //Ein Random Objekt wird erstellt, um zufallszahlen für die KI zu generieren
                    string tmp = possibilities[generator.Next(0, possibilities.Count)];     //Die KI wählt von allen möglichen Feldern ein Zufälliges, welches als String gespeichert ist
                    possibilities.Remove(tmp);                           //Um dieselbe Wahl nichtmehr treffen zu können wird das ausgewählte Element aus der Liste gelöscht!
                    spalte = tmp[0] - 48;                                //Vom string wird der Index 0 in einen Integer umgewandelt und in der Variable spalte gespeichert
                    zeile = tmp[1] - 48;                                 //Vom string wird der Index 1 in einen Integer umgewandelt und in der Variable zeile  gespeichert
                }
                try
                {
                    switch (box[zeile, spalte])                       //Diese Stelle wird, unabhängig ob man eine KI oder ein Spieler ist, ausgeführt. Im switch-case wird im Feld die Koordinate an der Stelle [Spalte [Zeile]] geändert, mehr dazu oben in der Skizze.
                    {
                        case '_':                                       //falls der case das Zeichen '_' annimmt 
                            box[zeile, spalte] = 'O';                //wird das Zeichen zu 'O' geändert, da ['_'= leer] und ['_'= verfehlt] bedeutet
                            break;
                        case '#':                                       //falls der case das Zeichen '_' annimmt
                            box[zeile, spalte] = 'X';                //wird das Zeichen zu 'X' geändert, da ['X'= getroffen] und ['#'= Schiff] bedeutet
                            winning_points -= 1;                        //falls ein Teil des Schiffes getroffen worden ist wird ein winning point abgezogen
                            break;
                        default:
                            Console.Write("\nYou've already fired there!\n");
                            return false;                               //Im falle das der Spieler eine Koordinate eingibt die genutzt worden ist, wird false für fehler zurückgegeben
                    }

                    return true;                                        //Im normalfall wird true zurückgegeben, für keine Fehler
                }
                catch (Exception)
                {

                    return false;
                }
            }

            public Field Clone_to_field_visible_to_player()             // Um es Fair zu halten wird hier ein Spielfeld generiert, welches die Schiffe nicht zeigt. Diese werden im Spiel-Loop benutzt.
            {
                char[,] alternativ = new char[10, 10];              //hier wird ein Leeres Feld erstellt die Alternative (Kopie)
                Field visible_to_player = new Field();              //Ein neues Feld Objekt wird erstellt.

                for (byte spalte = 0; spalte < 10; spalte++)
                {
                    for (byte zeile = 0; zeile < 10; zeile++)
                    {
                        if (box[spalte, zeile] == '#')
                        {
                            alternativ[spalte, zeile] = '_';        //alle Felder werden eingesetzt, '#' wird durch '_' ersetzt
                        }
                        else
                        {
                            alternativ[spalte, zeile] = box[spalte, zeile];
                        }
                    }
                }
                visible_to_player.box = alternativ;          //Die Boxen im alten Feld werden durch ihre Alternative ersetzt 
                return visible_to_player;                       //Das neue Feld wird zurückgegeben
            }

            public bool Has_lost()                                          //Hier wird zurückgegeben ob man verloren hat; true = verloren
            {
                if(winning_points == 0)                                 //Falls man 0 winningpoints hat hat man verloren, also wird hier der bool true zurückgegeben 
                {
                    return true;                                        //true = verloren
                }
                return false;                                           //false = noch nicht verloren
            }

            public static void DrawUI_inGame(Field playerField, Field visible_to_player) //Die Methode nimmt zwei Felder und bildet diese in der Konsole ab
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\t\t  A B C D E F G H I J\t\t  A B C D E F G H I J"); //Hier wird ein Teil der Benennung der Koordinaten gezeichnet
                for (byte spalte = 0; spalte < 10; spalte++)                                       //Die Schleife 1 speichert in "spalte" eine zahl von 1 bis 10
                {                                                           
                    Console.Write("\t\t"+spalte);
                    for (byte zeile = 0; zeile < 10; zeile++)                                       //Die Schleife 2 speichert in "zeile" eine zahl von 1 bis 10, dieser vorgang wird 10 wiederholt innerhalb Schleife 1. In Schleife 2 wird das Spieler Feld gezeichnet
                    {
                        Console.Write("|" + playerField.Get_box(spalte, zeile));         //In Schleife 2 wird im playerfield Array die Zeile und die Spalte der Ausgewählten zelle abgebildet hinter einem pipe zeichen
                    }

                    Console.Write("|\t\t" + spalte);                                 // Extra Abstände durch tabs, damit beide Felder korrekt angezeigt werden

                    for (byte zeile = 0; zeile < 10; zeile++)              //Die Schleife 3 speichert in "spalte" eine zahl von 1 bis 10, dieser vorgang wird 10 wiederholt innerhalb Schleife 1. In Schleife 2 wird das KI Feld gezeichnet
                    {
                        Console.Write("|" + visible_to_player.Get_box(spalte, zeile));             //In Schleife 3 wird im kiField Array die Zeile und die Spalte der Ausgewählten zelle abgebildet hinter einem pipe zeichen
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
                        Console.Write("|" + box[spalte, zeile]);         //In Schleife 2 wird im playerfield Array die Zeile und die Spalte der Ausgewählten zelle abgebildet hinter einem pipe zeichen
                    }
                    Console.WriteLine("|");
                }
            }
        
            public bool Place_ship(byte type, bool richtung, string koordinate) //Hier werden die Schiffe plaziert und ein bool zurückgegeben true = ein Fehler, false = alles Okay
            {
                char[,] backup = box;                                        //das Feld wird in backup gespeichert
                byte spalte = (byte)(Convert.ToInt32(koordinate[0]) - 97);      //hier wird der Buchstabe einer Koordinate in ein byte umgewandelt, um so den index herauszufinden. a ist als int 97 allerdings soll dies index 0 bedeuten, also wird - 97 gerechenet. Die Zahl wird in einem byte gespeichert, da es weniger platz braucht
                byte zeile = (byte)(Convert.ToInt32(koordinate[1]) - 48);       //hier die Nummer einer Koordinate in ein byte umgewandelt, um so den index herauszufinden. 0 ist als int 48 allerdings soll dies index 0 bedeuten, also wird - 48 gerechenet. Die Zahl wird in einem byte gespeichert, da es weniger platz braucht

                if (!richtung)                                                  //hier wird darauf geachtet, ob das Schiff vertikal oder Horizontal ist
                {
                    if (spalte + type < 11) for (byte extrapart = 0; extrapart < type; extrapart++)     //hier wird dei größe des Schiffes angepasst
                        {
                            if ((box[zeile, spalte + extrapart] == '#'))           //falls das ausgewählte Feld schon besetzt ist passiert:
                            {
                                box = backup;                                    //box wird mit dem backup überschrieben
                                return true;                                        //und es wird ein Fehler zurückgegeben
                            }
                            box[zeile, spalte + extrapart] = '#';                //Falls das Feld nicht schon beansprucht ist wird ein Schiffzeichen an dieser Stelle gesetzt
                        }
                    else
                    {
                        return true;                                            //Falls das Schiff nicht rein passt wird ein Fehler zurückgegeben
                    }
                }
                else                                                            //Im Else block wird der code ausgeführt welcher für die Vertikalen zuständig ist.
                {
                    if (zeile + type < 11) for (byte extrapart = 0; extrapart < type; extrapart++)     //hier wird dei größe des Schiffes angepasst
                        {
                            if ((box[zeile + extrapart, spalte] == '#'))           //falls das ausgewählte Feld schon besetzt ist passiert: 
                            {
                                box = backup;                                    //box wird mit dem backup überschrieben
                                return true;                                        //und es wird ein Fehler zurückgegeben
                            }
                            box[zeile + extrapart, spalte] = '#';                //Falls das Feld nicht schon beansprucht ist wird ein Schiffzeichen an dieser Stelle gesetzt
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
