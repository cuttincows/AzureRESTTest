from django.urls import path
from .views import ObjectDataCreate, ObjectDataDetail, index

urlpatterns = [
    path('', index, name='index'),
    path('objects/', ObjectDataCreate.as_view(), name='object-list'),
    path('objects/<int:pk>/', ObjectDataDetail.as_view(), name='object-detail'),
]
