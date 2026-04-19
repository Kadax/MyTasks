import { Component, inject, OnInit, signal } from '@angular/core';
import { KanbanBoardComponent } from "./components/kanban-board/kanban-board";
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet, RouterLinkWithHref } from '@angular/router';
import { AuthService } from './services/auth.service';
import { error } from 'console';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';




@Component({
  selector: 'app-root',
  imports: [CommonModule,
    MatIconModule,
    MatButtonModule,
    RouterOutlet, RouterLinkWithHref],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit{
  protected readonly title = signal('MyTask');
  private router = inject(Router);


  constructor(public authService: AuthService){
  }

  ngOnInit(): void {
    this.checkLogIn();
  }

  toggleDarkMode() {
    document.body.classList.toggle('dark-mode');
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
