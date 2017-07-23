using System;

using CLAP;

namespace CeConsole
{
    /*
     * Requests:
     *      Command "create"
     *      Command "delete"
     *      Query "watch"
     *      Command "vote"
     * 
     * $ ceconsole.exe connect -address http://abcd.com
     * Connected to http://abcd.com
     * $
     * 
     * oder
     * 
     * $ ceconsole.exe connect
     * Connected to http://localhost:8080
     * $
     * 
     * oder
     * 
     * $ ceconsole.exe
     * Connected to http://localhost:8080
     * $
     * 
     * $ ceconsole.exe create
     * a(dd, r(emove, l(ist, s(ubmit: a
     * User story text: X
     * a(dd, r(emove, l(ist, s(ubmit: a
     * User story text: Y
     * a(dd, r(emove, l(ist, s(ubmit: l
     * 1. X
     * 2. Y
     * a(dd, r(emove, l(ist, s(ubmit: s
     * sprint created with id "abcde"
     * $
     * 
     * $ ceconsole.exe delete abcde
     * sprint deleted with id "abcde"
     * $
     * 
     * $ ceconsole.exe watch abcde
     * Total weight of sprint "abcde":
     * 1. Y
     * 2. X
     * Number of votings: 3
     * 
     * ...automatic refresh after 10 seconds...
     * 
     * Total weight of sprint "abcde":
     * 1. Y
     * 2. X
     * Number of votings: 3
     * CTRL^C
     * $
     * 
     * $ ceconsole.exe vote abcde
     * 1.a) X
     * 1.b) Y
     * your vote: a
     * 2.a) X
     * 2.b) Z
     * your vote: b
     * ...
     * voting submitted for sprint "abcde"
     * $
     * 
     * oder
     * 
     * ...
     * voting submitted for sprint "abcde"
     * *** inconsistency deteced on this weighting:
     * 3.a) Y
     * 3.b) Z
     * your vote: a
     * voting submitted for sprint "abcde"
     * $
     * 
     * oder
     * 
     * ...
     * voting submitted for sprint "abcde"
     * *** inconsistency deteced on weighting 3!
     * 1.a) X
     * 1.b) Y
     * your vote: a
     * 2.a) X
     * 2.b) Z
     * your vote: b
     * ...
     * voting submitted for sprint "abcde"
     * $
     * 
     */
    class MainClass {
        public static void Main(string[] args) {
            Parser.Run<CLIPortal>(args);
        }
    }
}