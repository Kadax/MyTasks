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
import {MatChipsModule} from '@angular/material/chips';


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
    MatDialogModule,
    MatChipsModule
  ]
})


export class KanbanBoardComponent implements OnInit {


  constructor(public taskService: TaskService,
              private dialog: MatDialog,
              private cdr: ChangeDetectorRef
  ) {}

  tasksByStatus = new Map<number, MyTask[]>();
  isUpdate: boolean = true;


  ngOnInit(): void {
    this.update();
    setInterval(() => {
      if(this.isUpdate){
        this.update();
        this.isUpdate = false;
        this.cdr.markForCheck();
      }
    }, 1000);

    this.taskService.GetListTasks().subscribe(
      data=>{
        this.isUpdate = true;
      }
    );

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

      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);

      this.taskService.updateTaskOrder(event.container.data);


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

  openTaskDialog(task: MyTask | null) {
    const dialogRef = this.dialog.open(TaskDialogComponent, {
      width: '500px',
      data: task
    });


    dialogRef.componentInstance.taskChangeEvent.subscribe(
      (task) => {
        this.isUpdate = true;

        // Handle the new or updated task
        console.log('Task created/updated:', task);
     });
  }

  AddTime(task: MyTask, time: number){
    if(task.id && task.id != 0){
      this.taskService.AddTime(task.id, time).subscribe(
        (data)=>{
          task.totalTime = data;
           this.cdr.markForCheck();
        },
        (error)=>{
          alert(error);
        }
      )
    }

  }

totalTime(time: number | undefined): string {
  if(time){
    let h = Math.floor(time/60);
    let m = time - h*60;

    return h+"h "+ (m!=0? m+'m' : '');
  }
  return "-";
}


}
