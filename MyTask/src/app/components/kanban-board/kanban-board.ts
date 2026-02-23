import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CdkDragDrop, DragDropModule, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Task, TaskStatus } from '../../models/task.model';
import { TaskService } from '../../services/task.service';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import {MatInputModule} from '@angular/material/input';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { TaskDialogComponent } from '../task-dialog/task-dialog';


@Component({
  selector: 'app-kanban-board',
  templateUrl: './kanban-board.html',
  styleUrls: ['./kanban-board.scss'],
  imports: [
    CommonModule,
    DragDropModule,
    MatCardModule,
    MatListModule,
    MatIconModule,
    MatInputModule,
    MatDialogModule
  ]
})


export class KanbanBoardComponent implements OnInit {
  statuses = [
    { key: TaskStatus.TODO, label: 'Todo' },
    { key: TaskStatus.IN_PROGRESS, label: 'In Progress' },
    { key: TaskStatus.DONE, label: 'Done' }
  ];

  constructor(public taskService: TaskService,
              private dialog: MatDialog,
              private cdr: ChangeDetectorRef
  ) {}

  tasksByStatus = new Map<string, Task[]>();
  isUpdate: boolean = false;


  ngOnInit(): void {
    this.update();
    setInterval(() => {
      if(this.isUpdate){
        this.update();
        this.isUpdate = false;
        this.cdr.markForCheck();
      }
    }, 1000);
  }

  update(){
    this.statuses.forEach(s => {
      this.tasksByStatus.set(s.key, this.taskService.getTasksByStatus(s.key));
    });
  }

  ngAfterViewInit() {

  }


  get statusKeys(): string[] {
    return this.statuses.map(s => s.key);
  }

  drop(event: CdkDragDrop<any[]> | any, targetStatusKey: string) {
    if (event.previousContainer === event.container) {
      // re‑order within the same column
      this.taskService.updateTaskOrder(event.item.data.id, event.currentIndex);

      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      // move to a different column
      this.taskService.updateTaskStatus(event.item.data.id, this.statuses.find(i=>i.key === targetStatusKey)?.key)



      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }
  }

  openTaskDialog() {
    const dialogRef = this.dialog.open(TaskDialogComponent, {
      width: '500px'
    });


    dialogRef.componentInstance.taskChangeEvent.subscribe((task) => {
      this.taskService.addTask(task);
      this.isUpdate = true;

       // Handle the new or updated task
       console.log('Task created/updated:', task);
     });
  }


}
