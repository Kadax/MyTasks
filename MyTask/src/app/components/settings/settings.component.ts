import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckbox, MatCheckboxModule } from '@angular/material/checkbox';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatExpansionModule} from '@angular/material/expansion';
import { TaskService } from '../../services/task.service';
import { Status, TypeTask } from '../../models/task.model';
import { RouterLinkWithHref } from '@angular/router';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
  imports: [
    CommonModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatFormFieldModule,
    FormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckbox,
    MatCheckboxModule,
    MatExpansionModule,
    RouterLinkWithHref
  ]
})
export class SettingsComponent implements OnInit {

  constructor(
    public taskService: TaskService,
    private cdr: ChangeDetectorRef
  ) {

  }

  readonly panelOpenState = signal(false);

  ngOnInit() {

  }

  save(state: Status) {
    this.taskService.SaveStatus(state).subscribe(
      (data)=>{
        state.id = data.id;
      },
      (error)=>{
        console.log(error);
      }
    )
  }


  saveType(type: TypeTask) {
    this.taskService.SaveTypeTask(type).subscribe(
      (data)=>{
        type.id = data.id;
      },
      (error)=>{
        console.log(error);
      }
    )
  }

  addType(){
    this.taskService.AddTypeTask();
  }

  delete(type: TypeTask){
    if(type.id != 0){
      this.taskService.deleteTypeTask(type.id).subscribe(
        (data)=>{

        },
        (error)=>{
          alert(error.error);
        }
      )
    }

    this.taskService.types = this.taskService.types.filter(i=>i.id != type.id);
  }



}
