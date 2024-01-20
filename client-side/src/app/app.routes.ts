import { Routes } from '@angular/router';
import { ClothesComponent } from './components/clothes/clothes.component';
import { DevicesComponent } from './components/devices/devices.component';
import { FoodsComponent } from './components/foods/foods.component';
import { IngredientsComponent } from './components/ingredients/ingredients.component';
import { HomeComponent } from './components/home/home.component';
import { MaterialsComponent } from './components/materials/materials.component';

export const routes: Routes = [
  {path: 'clothes', component: ClothesComponent},
  {path: 'devices', component: DevicesComponent},
  {path: 'foods', component: FoodsComponent},
  {path: 'ingredients', component: IngredientsComponent},
  {path: 'materials', component: MaterialsComponent},
  {path: '', component: HomeComponent}
];
