import { Routes } from '@angular/router';
import { KanbanBoardComponent } from './components/kanban-board/kanban-board';
import { AuthenticationComponent } from './components/authentication/authentication';

export const routes: Routes = [
    {path: '', component: KanbanBoardComponent},
    {path: 'auth', component: AuthenticationComponent},
    { path: "**", redirectTo: "/"}
];
