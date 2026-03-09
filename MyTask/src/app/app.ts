import { Component, inject, OnInit, signal } from '@angular/core';
import { KanbanBoardComponent } from "./components/kanban-board/kanban-board";
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from './services/auth.service';
import { error } from 'console';




@Component({
  selector: 'app-root',
  imports: [CommonModule,
    // KanbanBoardComponent,
    RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit{
  protected readonly title = signal('MyTask');
  private router = inject(Router);


  constructor(private authService: AuthService){
  }

  ngOnInit(): void {
    this.checkLogIn();
  }

  checkLogIn(){
    this.authService.CheckLogin().subscribe(
      data=>{
        this.router.navigate(['/']);
      },
      error=>{
        this.router.navigate(['/auth']);
      }
    )
  }



}
