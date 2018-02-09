# TaskMaster API
###### Keep your tasks organized

---

## Task Item Model:
This is the individual todo item on your todo list.

#### Id (int)
Leave this blank when creating a new task. The database will fill this out.

#### Created (long)
Also leave this blank. This gets set with the current unicode timestamp when the task is created.

#### DueBy (long)
Fill this one in if there is a set time the item should be completed by. Unicode Timestamp is expected.

#### RemindAt (long)
Fill this one in if you want to be reminded via pushbullet. Expects Unicode Timestamp.

#### Description (string)
Enter in whatever text data you want associated with your task here.

## API Requests:
Send your requests to (ADDRESSPLACEHOLDER)/api/. These routes are currently supported.

#### GET: (ADDRESSPLACEHOLDER)/api/TaskItem/
This returns a list of all task items. In later versions you will be able to optionally specify filters.

#### GET: (ADDRESSPLACEHOLDER)/api/TaskItem/{id}
This returns one task item from the database.

#### POST: (ADDRESSPLACEHOLDER)/api/TaskItem/
Expects a Json serialized TaskItem model in the body. Id and Created will be overwritten if present.

#### PUT: (ADDRESSPLACEHOLDER)/api/TaskItem/
Expects a Json serialized TaskItem model in the body. if ID is present and exists in database, the item will be altered with any new keys with non-null values passed in. "Created" timestamp will not be modified. If the ID is not present, then the item will be created in the database.

#### DELETE: (ADDRESSPLACEHOLDER)/api/TaskItem/{id}
If a TaskItem exists at this place in the database, it will be removed.

---

## Category Model:
Categories exist to organize your tasks.

#### Id (int)
Leave blank, filled by database

#### Name (string) [Required]
A display name for your category.

---

## TaskCategoryAssoc (Association) Model:
In order to add a TaskItem to a Category, you need to register an Association.
First find the ID of your TaskItem and Category (somehow I dunno figure it out.)
Then simply create a new TaskCategoryAsssoc with a Category matching the Category ID and TaskItem matching the Task Item Id.

#### Id (int)
This only exists for the database, just ignore it.

#### Category (int)
The ID of the Category we are associating.

#### TaskItem
The ID of the Task you are adding to that Category.
