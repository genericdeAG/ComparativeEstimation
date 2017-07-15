Hier alle Bibliotheken ablegen, die von den Projekten in /src gebraucht werden.

Solange NuGet-Bibliotheken benutzt werden, ist das nicht nötig. Die können bei den Projekten liegen, weil sie aus dem globalen NuGet-Repo beim Build aktualisiert werden. Und natürlich soll hier auch nicht der .NET Fx liegen.

Alle anderen Bibliotheken jedoch sollten nicht global aus irgendwelchen Verzeichnissen auf dem pers. Rechner ode dem GAC referenziert werden, sondern hier liegen. Nur so sind ihre Versionen und ihr Vorhandensein unter Kontrolle.