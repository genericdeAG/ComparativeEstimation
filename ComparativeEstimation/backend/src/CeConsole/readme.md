# Console Client Usage
## Mit Server verbinden (PO, Dev)
Einmalig

`$ ceconsole.exe connect -a:http://...`

aufrufen. Damit wird für weitere Requests die Endpunktadresse des Servers festgelegt (Datei `RESTProvider.config`). Es muss also nur einmal `connect`et werden, solange die Serveradresse gleich bleibt.

Die Adresse des Servers in der Cloud bei dropstack ist derzeit `http://wdrssjin.cloud.dropstack.run`.

Wenn der Parameter `-a:` fehlt, wird `http://localhost:8080` angenommen.

## Sprint anlegen (PO)
`$ ceconsole.exe create`

User Stories Zeile für Zeile eingeben. Leere Zeile schließt Liste ab.

Die User Stories werden an den Server übertragen. Das Ergebnis ist eine Sprint Id.

## Gesamtgewichtung eines Sprint beobachten (PO)
`$ ceconsole.exe watch -id:<sprintId>`

Es wird die aktuelle Gesamtgewichtung angezeigt.

## User Stories gewichten (Dev)
`$ ceconsole.exe vote -id:<sprintId>`

Es wird für jedes User-Story-Vergleichspaar die Gewichtung einzeln erfragt. Nach dem letzten Paar wird das Voting autom. an den Server übertragen.

Falls im Voting eine Inkonsistenz vorliegt, wird das zurückgemeldet.

## Sprint löschen (PO)
`$ ceconsole.exe delete -id:<sprintId>`

Der Sprint wird auf dem Server gelöscht.
