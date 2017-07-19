# REST Protokoll
## Sprint angelegen (PO)
```
POST /api/sprints
In:
{["X", "Y", "Z"]} // jeder Eintrag eine User Story
Out:
200 OK
abcd1234 // ID des Sprint
```

## Vergleichspaare abholen (Dev)
```
GET /api/comparisonpairs/<sprintId>
In:
Out:
200 OK
{
    "sprintId":"abcd1234",
    "pairs": [
        {
            "Id": "1",
            "A":  "X",
            "B":  "Y"
        },
        ...
    ]
}
```
## Voting einreichen (Dev)
```
POST /api/votings/<sprintId>
In:
{
    "voterId": "...",
    "weightings" : [
        {
            "Id": "1",
            "Selection": "A"
        },
        ...
    ]
}
Out:
// im Erfolgsfall:
200 OK
// im Falle von Inkonsistenzen:
422 Unprocessable
{
    [ "1", ... ] // Ids der inkonsistenten Paare
}
```
## Gesamtgewichtung abrufen (PO)
```
GET /api/sprints/<sprintId>
In:
Out:
200 OK
{
    "TotalWeighting": ["Z", "X", "Y"],
    "NumberOfVotings": 3
}
```
## Sprint löschen (PO)
```
DELETE /api/sprints/<sprintId>
In:
Out:
200 OK
```