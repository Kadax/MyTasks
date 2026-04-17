import { Routes } from '@angular/router';
import { KanbanBoardComponent } from './components/kanban-board/kanban-board';
import { AuthenticationComponent } from './components/authentication/authentication';
import { SettingsComponent } from './components/settings/settings.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';

export const routes: Routes = [
    {path: '', component: KanbanBoardComponent},
    {path: 'auth', component: AuthenticationComponent},
    {path: 'settings', component: SettingsComponent},
    {path: 'changepass', component: ChangePasswordComponent},
    //{ path: "**", redirectTo: "/"}
];
