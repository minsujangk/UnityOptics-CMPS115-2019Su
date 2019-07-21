## General Object

#### Location (on Firebase Storage)

{objName}/viewdata_{time}.json

-> one file for each object

#### Attributes

- *string* objName: name of an object; can be used as id of ad objects.
- *float* time: time elapsed from start of game.
- *float* duration: duration of continuous viewing on an object.
- *float* minDist: minimum distance between player and the object in a viewing.
- *float* maxDist: maximum distance between player and the object in a viewing.



## Reading Ads

#### Location (on Firebase Storage)

read/read_{time}.json

-> one file for each play, so a file has whole data for all objects

#### Attributes

- *string* objName: name of an object; can be used as id of ad objects.
- *float* time: time elapsed from start of game.
- *float* duration: duration of continuous reading of an advertisement.

## Reading Ads

#### Location (on Firebase Storage)

{objName}/pickup/pickup_{time}.

-> one file for each object

#### Attributes

- *string* objName: name of an object; can be used as id of ad objects.
- *float* time: time elapsed from start of game.
- *int* type: type of action. 1=pickup, 2=throw

