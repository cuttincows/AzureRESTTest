from django.shortcuts import render
from rest_framework import generics
from .models import ObjectData
from .serializers import ObjectDataSerializer
from .forms import ColorForm
from django.views.generic.edit import UpdateView

from django.http import HttpResponse, HttpResponseRedirect

def index(request):
    if request.method == "POST":
        form = ColorForm(request.POST)
        if form.is_valid():
            # Update data
            if (ObjectData.objects.all().count() == 0):
               newObj = ObjectData(title="Cube", color="FF0000").save()
            dataObject = ObjectData.objects.all()[0]
            dataObject.color = form.cleaned_data['color']
            dataObject.save()

            # Redirect
            return HttpResponseRedirect("")
    else:
        form = ColorForm()
    #   return HttpResponse("No data")
    # return HttpResponse("Testdata")
    return render(request, "updatecolor.html", {"form": form})

#def updatecolor(request):
def updatecolor(request):
    if request.method == 'POST':
        form = ColorForm(request.POST)
        if form.is_valid():
            color = form.cleaned_data['color']
            return redirect('updatecolor')

class ObjectDataCreate(generics.ListCreateAPIView):
    queryset = ObjectData.objects.all()
    serializer_class = ObjectDataSerializer
   
class ObjectDataDetail(generics.RetrieveUpdateDestroyAPIView):
    queryset = ObjectData.objects.all()
    serializer_class = ObjectDataSerializer
