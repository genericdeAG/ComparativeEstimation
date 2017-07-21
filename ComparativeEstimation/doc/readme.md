# REST Protokoll
## Sprint angelegen (PO)
```
POST /api/sprints
In:
Content-Type: application/json
["X", "Y", "Z"] // jeder Eintrag eine User Story
Out:
200 OK
abcd1234 // ID des Sprint
```

## Vergleichspaare abholen (Dev)
```
GET /api/comparisonpairs/{sprintId}
In:
Out:
200 OK
Content-Type: application/json
{
    "SprintId":"abcd1234",
    "Pairs": [
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
Content-Type: application/json
{
    "VoterId": "...",
    "Weightings" : [
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
Content-Type: application/json
{
    "SprintId": "42",
    "ComparisonPairId": "1" // Inkonsistenz tritt hier auf
}
```
## Gesamtgewichtung abrufen (PO)
```
GET /api/sprints/<sprintId>
In:
Out:
200 OK
Content-Type: application/json
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