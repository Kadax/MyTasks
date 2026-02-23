import { Component, Injectable, Input, Output, EventEmitter, OnInit, inject } from '@angular/core';
import { MAT_DIALOG_DATA,
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle } from '@angular/material/dialog'; //Import Dialog
import { Task } from '../../models/task.model';
import { TaskStatus } from '../../models/task.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select'
import {MatButtonModule} from '@angular/material/button'
import {FormsModule} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';


@Component({
  selector: 'app-task-dialog',
  templateUrl: './task-dialog.html',
  styleUrl: './task-dialog.scss',
  imports: [
    CommonModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatFormFieldModule,
    FormsModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions
  ]
})

export class TaskDialogComponent implements OnInit {



  constructor(private dialogRef: MatDialogRef<TaskDialogComponent>)
  {

  }

  @Output() taskChangeEvent = new EventEmitter<Task>();

  data = inject<Task>(MAT_DIALOG_DATA);


  ngOnInit(): void {
    if(this.data==null){
      this.data = { id: 0, title: '', description:'', status: TaskStatus.TODO, orderNumber: 100 };
    }
  }

  save(): void {

    this.taskChangeEvent.emit(this.data);

    this.dialogRef.close();
  }

  cancel(): void {
    this.dialogRef.close();
  }

}
