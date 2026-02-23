import { Component, signal } from '@angular/core';
import { KanbanBoardComponent } from "./components/kanban-board/kanban-board";
import { CommonModule } from '@angular/common';




@Component({
  selector: 'app-root',
  imports: [CommonModule,
            KanbanBoardComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('MyTask');
}
