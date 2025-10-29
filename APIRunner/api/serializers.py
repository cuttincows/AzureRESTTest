from rest_framework import serializers
from .models import ObjectData

class ObjectDataSerializer(serializers.ModelSerializer):
    class Meta:
        model = ObjectData
        fields = '__all__'