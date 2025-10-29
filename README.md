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
