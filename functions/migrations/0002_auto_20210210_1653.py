# Generated by Django 3.1.5 on 2021-02-10 16:53

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('functions', '0001_initial'),
    ]

    operations = [
        migrations.RemoveConstraint(
            model_name='function',
            name='function_is_unique',
        ),
        migrations.RenameField(
            model_name='function',
            old_name='app',
            new_name='owner',
        ),
        migrations.AddConstraint(
            model_name='function',
            constraint=models.UniqueConstraint(fields=('owner', 'name'), name='function_is_unique'),
        ),
    ]