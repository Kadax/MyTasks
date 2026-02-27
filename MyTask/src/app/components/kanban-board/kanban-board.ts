import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CdkDragDrop, DragDropModule, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { MyTask, Status } from '../../models/task.model';
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


  constructor(public taskService: TaskService,
              private dialog: MatDialog,
              private cdr: ChangeDetectorRef
  ) {}

  tasksByStatus = new Map<number, MyTask[]>();
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
    this.taskService.statuses.forEach(s => {
      this.tasksByStatus.set(s.id, this.taskService.getTasksByStatus(s));
    });
  }

  ngAfterViewInit() {

  }



  get statusKeys(): string[] {
    return this.taskService.statuses.map(s => s.name);
  }

  drop(event: CdkDragDrop<any[]> | any, targetStatusKey: number) {
    if (event.previousContainer === event.container) {
      // re‑order within the same column
      this.taskService.updateTaskOrder(event.item.data.id, event.currentIndex);

      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      // move to a different column
      this.taskService.updateTaskStatus(event.item.data.id, this.taskService.statuses.find(i=> i.id === targetStatusKey))



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
