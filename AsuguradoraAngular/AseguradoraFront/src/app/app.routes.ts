import { Routes } from '@angular/router';
import { InicioComponent } from './Views/inicio/inicio.component';
import { ClienteComponent } from './Views/cliente/cliente.component';

export const routes: Routes = [

    {path:'', component:InicioComponent},
    {path:'inicio', component:InicioComponent},
    {path:'cliente/:id', component:ClienteComponent},

];
