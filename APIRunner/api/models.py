from django.db import models

class ObjectData(models.Model):
    title = models.CharField(max_length=200)
    color = models.CharField(max_length=20)
    
    def __str__(self):
        return self.title
