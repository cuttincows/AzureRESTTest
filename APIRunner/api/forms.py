from django import forms

class ColorForm(forms.Form):
    title = forms.CharField(label='Object title', max_length=100)
    color = forms.CharField(label='Object color', max_length=20)