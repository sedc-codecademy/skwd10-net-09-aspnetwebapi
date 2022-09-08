import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CreteNoteComponent } from "./components/crete-note/crete-note.component";
import { GetNotesComponent } from "./components/get-notes/get-notes.component";
import { HomeComponent } from "./components/home/home.component";
import { LoginComponent } from "./components/login/login.component";
import { RegisterComponent } from "./components/register/register.component";

const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' }, // default starting route
    { path: 'home', pathMatch: 'full', component: HomeComponent },
    { path: 'register', pathMatch: 'full', component: RegisterComponent },
    { path: 'login', pathMatch: 'full', component: LoginComponent },
    { path: 'notes', pathMatch: 'full', component: GetNotesComponent },
    { path: 'create', pathMatch: 'full', component: CreteNoteComponent }
]

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}