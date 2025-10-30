# Test application for my Ballard Games interview!

to run:
- Verify python is installed and in the path
- Run the following, from the project root:
```
pip install django
pip install djangorestframework
python .\APIRunner\manage.py runserver
```

This should create a local server, defaulted at http://127.0.0.1:8000.  If you get a different run location, swap it out in subsequent commands
- Navigate to http://127.0.0.1:8000/api/
- This should be the live site!  Enter anything in the object title field, then entry a valid html color string (WITHOUT the #, ex ff0000, aa11bb)
- Hit send!  This should update the API's database to be pulled from Unity

On the Unity side, simply load the project (any recent Unity version should work, but I used 6000.2.9f1, as reflected in ProjectVersion.txt under ProjectSettings)
- If the url given in the earlier step isn't the default, find the Cube object in the heirarchy in the SampleScene, find the SetColorFromServer MonoBehavior on that object, and update it to the correct URL

<img width="1918" height="1035" alt="image" src="https://github.com/user-attachments/assets/b4872c4c-a9c6-4d97-9289-20b6539944de" />

If you setup everything correctly, submitting a valid color in the form should update the running Unity Editor's cube!

There's also an in-editor tool - go to `Tools/Color editor tool` in Unity to open it, and if you select a color in the tool and hit "submit", the local server is updated with the color you selected.

<img width="620" height="315" alt="image" src="https://github.com/user-attachments/assets/455db860-843a-4c9e-bf24-53e6a509c038" />



## How's it work?
- On the Django side, I'm creating a template form that takes just the input, and saves it to the 0th entry of the ObjectData list.  This is genericized on the Django side, so any kind of database could be swapped out to manage this data
  - If no cubes exist, a dummy cube ObjectData instance is created to be able to modify for this application.  The default cube should already be saved in the sqlite database packaged in this repo, but just in case, the form index view should make a new one for you 
  - Check ApiRunner\views.py for an entry point in code
- Unity communicates with the service with a standard web request, as the cube color isn't secured for this demo no login information is needed
  - GET is used, as no data is being updated on the server from the Unity side
  - The server is pinged every quarter second to see if the color was updated, this frequency can be changed on the GameObject in Unity
  - A basic substring call is able to parse out the 0th object (which is the only one relevant in this demo), reconstructs into an ObjectColor instance {string title, string color}, then uses ColorUtility.TryParseHtmlString to turn the entered color into something Unity can use
 
## Future Proofing
- Security on the Django side - utilize a login so not just anybody can update the cube color
- Register the cube color to a user account with a unique ID, so that each user can customize the color of their cube
- Genericize the color interface to allow multiple colors to be associated with the same user
- Scalability - use something other than SQLite
- More robust response filtering (for n number of objects vs the 1 the demo enables)
- Data sanitization and defaults: don't let django set an invalid color, give Unity a default color to use when django doesn't have a valid color
