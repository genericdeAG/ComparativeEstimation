# Comparative Estimation - Schätzen einmal anders
*Comparative Estimation (CE)* bedeutet, beim Schätzen den "Dingen" keinen absoluten Wert zu geben, sondern ihre Werte nur paarweise zu vergleichen und sie danach in eine Ordnung zu bringen.

## Das Verfahren

Beispiel: Gegeben seien die User Stories A, B, C, D.

Absolute Schätzung weißt ihnen konkrete Werte für den benötigten Umsetzungaufwand oder das in ihnen steckende Risiko oder ihre Wichtigkeit oder ihre Nützlichkeit für den Kunden zu.

Story Points (SP) sind eine übliche Einheit für Aufwände, z.B.

* A = 5 SP
* B = 3 SP
* C = 15 SP
* D = 8 SP

In absteigender Reihenfolge ergibt sich die Ordnung C,D,A,B.

Bei der der CE ist die Bewertung nicht absolut, sondern relativ. Hier erfolgt Herstellung der Ordnung in zwei Phasen:

* Phase 1: Paarweiser Vergleich. Es wird jeweils angegeben, welche Option *vergleichsweise* und *relativ* mehr Gewicht hat:
  * A:B = A
  * A:C = C
  * A:D = D
  * B:C = C
  * B:D = D
  * C:D = C
* Phase 2: Topologische Sortierung der Vergleiche. Das Resultat ist hier ebenfalls C,D,A,B.

Absolute Werte finden, ist schwer. Jeweils nur zwei Optionen miteinander zu vergleichen, ist hingegen leicht.

Die Zahl der Vergleiche wächst mit der Zahl der Optionen jedoch recht schnell. CE bietet sich daher nur für vielleicht max. 10 Optionen an. (Die Zahl der Vergleichspaare berechnet sich nach der Form *(n^2 - n)/2*.

Viele paarweise Vergleiche müssen jedoch nicht unbedingt hohen Schätzaufwand bedeuten. Für viele Paare lässt sich "auf einen Blick" entscheiden, welche Option die Gewichtigere ist.

Außerdem regt CE dazu an, genau hinzuschauen. Die Diskussionen zur Gewichtung von Optionen, die sich aus CE entspinnen, sind fokussierter.

Es kann beim Vergleich allerdings zu Inkonsistenzen kommen. Beispiel:

* A:B = A
* A:C = C
* A:D = D
* B:C = B // Inkonsistenz!

Der vierte Vergleich ist inkonsistent zum Ergebnis von A:B und A:C.

Inkonsistenzen sind jedoch kein Bug des Verfahrens, sondern ein Feature. Wenn kein Flüchtigkeitsfehler vorliegt, wird durch sie sehr präzise deutlich, worüber nochmal nachgedacht werden sollte oder Diskussionsbedarf im Team herrscht.

### Schätzen in Teams
In Teams schätzt jedes Teammitglied die in Frage stehenden Optionen zunächsts für sich nach CE. Beispiele für Einzelschätzungen:

* Peter: C,D,A,B
* Paul: D,C,A,B
* Maria: D,A,C,B

Das Gesamtergebnis ergibt sich dann durch eine Addition der summierten Prioritäten.

Die Prioritäten sind für Peters Schätzung:

1. C
2. D
3. A
4. B

Bei Peter hat C die Priorität 1, bei Paul 2, bei Maria 3. Die Summe der Prioritäten für C ist damit 6.

* A = 3+3+2 = 8
* B = 4+4+4 = 12
* C = 1+2+3 = 6
* D = 2+1+1 = 4

Als Ergebnis für die Teamschätzung ergibt sich also nach aufsteigender Gesamtpriorität: D,C,A,B

## Die Anwendung
Zweck der Anwendung ist es, das CE-Verfahren für Teams zu unterstützen. Verteilte Teams sollen nach Aufforderung durch z.B. einen Product Owner (PO) User Stories nach Aufwand schätzen.

1. Der Produkt Owner erfasst eine Liste von User Stories.
2. Der Produkt Owner lädt Teammitglieder zum Schätzen ein.
3. Teammitglieder schätzen die User Stories mit CE.
4. Der Produkt Owner beobachtet die Entwicklung der Teamschätzung.

Product Owner und weitere Teammitglieder sitzen zu verschiedenen Zeiten an verschiedenen Rechnern an unterschiedlichen Orten. "Schätzrunden" dauern daher u.U. einige Stunden oder gar Tage.

### 1. User Stories erfassen
Der PO erfasst User Stories im PO-Client der Anwendung. Jede User Story wird durch einen kurzen Text beschrieben. Weitere Details sollten den Teammitgliedern auf andere Weise mitgeteilt werden.

Die Erfassung soll einfach sein, z.B. ist Nummerierung ist nötig. Jede User Story soll für sich durch ihre Beschreibung sprechen.

Alle User Stories zusammen stellen eine Schätzrunde dar.

### 2. Teammitglieder einladen
Der PO veröffentlicht die Schätzrunde und lädt Teammitglieder dazu ein.

Die Einladung besteht (zunächst nur) aus einer ID für die Schätzrunde. Diese ID kann durch durch die Anwendung generiert werden. Sie wird den Teammitgliedern über einen separaten Kanal mitgeteilt, z.B. Email oder Slack-Nachricht.

### 3. Teammitglieder schätzen
Mit der ID der Schätzrunde rufen die Teammitglieder den Team-Client der Anwendung auf. Der zeigt ihnen sofort eine Liste von User-Story-Paaren.

Für jedes Paar legt das Teammitglied fest, welche Option es für gewichtiger hält.

Welche Paare noch nicht gewichtet wurden, sollte deutlich erkennbar sein.

Sobald alle Paare gewichtet wurden, reicht das Teammitglied seine Schätzung ein. Sollte sich dabei herausstellen, dass es Inkonsistenzen gibt, werden die Paare hervorgehoben, bei denen diese auftreten.

### 4. Entwicklung der Schätzrunde beobachten
Der PO kann jederzeit den PO-Client mit einer Schätzrunden-ID starten und sieht dann den aktuellen Stand der Teamschätzung:

* nach Gesamtgewichtung geordnete Liste der User Stories
* Anzahl der abgegebenen Schätzungen

### Ausblick
* PO gibt bei einer Schätzrunde seine Email-Adresse an und auch die Email-Adressen der Teammitglieder.
  * Die Teammitglieder geben bei ihrer Schätzung nicht nur die ID, sondern auch ihre Email-Adresse an.
  * Teammitglieder können mehrfach Schätzungen abgeben, wobei nur die jeweils letzte gezählt wird.
  * Der aktuelle Stand zeigt auch die Zahl der Teammitglieder.
  * Teammitglieder werden per Email zur Schätzrunde eingeladen.
  * Der PO wird über abgegebene Schätzungen per Email informiert.
* Der PO kann eine Schätzrunde abschließen. Dann werden keine weiteren Schätzungen von Teammitglieder mehr akzeptiert.