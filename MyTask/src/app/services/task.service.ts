import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { MyTask, Status } from '../models/task.model';
import { HttpClient } from '@angular/common/http';
import { urls } from '../const.ts';
import { TimeSpentDTO } from '../models/TimeSpentDTO';

@Injectable({ providedIn: 'root' })
export class TaskService {
  public tasksSubject = new BehaviorSubject<MyTask[]>([]);
  tasks$ = this.tasksSubject.asObservable();

  constructor() {
    // При старте приложения подставляем 5 примеров
    const initial: MyTask[] = [];
    this.tasksSubject.next(initial);
    this.GetListStatuses();
  }

  private http = inject(HttpClient);

  statuses: Status[] = [];

  GetListStatuses(){
    this.http.get<Status[]>(urls.server + 'TaskStatus').subscribe(
      date=>{
        this.statuses = date;
      }
    );
  }

  GetListTasks(){
    let req =  this.http.get<MyTask[]>(urls.server + 'Tasks')
    req.subscribe(
      (data)=>{
        this.tasksSubject.next(data);
      },
      (error)=>{
        console.log(error);
      }
    );

    return req;
  }

  /** Получаем задачи по статусу */
  getTasksByStatus(status: Status): MyTask[] {
    return this.tasksSubject.getValue().filter(t => t.statusId === status.id).sort((a, b) => a.orderNumber - b.orderNumber);
  }

  AddTime(taskId: number, time: number){
    let t = new TimeSpentDTO();

    t.taskId = taskId;
    t.duration = time;

    return this.http.post<number>(urls.server + 'TimeSpent', t);

  }

  updateTaskOrder(data: any[]){

    console.log(data);

    let tasks = data.map((i, index) => ({
      taskId: i.id,
      order: index
     }));

    this.http.post(urls.server+"OrderTasks", tasks).subscribe(
      (data)=>{

      },
      error=>{

      }
    );

  }


  /** Перемещаем задачу в новый статус */
  updateTaskStatus(taskId: number, newStatus: Status | undefined) {
    if(newStatus){
      const tasks = this.tasksSubject.getValue().map(t =>
        t.id === taskId ? { ...t, statusId: newStatus.id } : t
      );

      this.http.post(urls.server+"ChangeState?TaskId="+taskId+"&StateId="+newStatus.id,null).subscribe(
        data=>{

        },
        error=>{
          console.log(error);
        }
      )

      this.tasksSubject.next(tasks);
    }
  }


  /** Добавляем задачу */
  addTask(task: Omit<MyTask, 'id'>) {
    const newTask: MyTask = { ...task, id: 0 };

    let req =  this.http.post<MyTask>(urls.server + 'Tasks', newTask);


    return req;
  }

  updateTask(task: MyTask){
    let req =  this.http.put<MyTask>(urls.server + 'Tasks/'+task.id, task);
    // req.subscribe(
    //   (data)=>{
    //     let t = this.tasksSubject.value.find(i=>i.id == task.id);
    //     if(t){
    //       t = data;
    //     }
    //   },
    //   (error)=>{

    //   }
    // );

    return req;
  }




  /** Удаляем задачу */
  deleteTask(taskId: number, isArhive: boolean) {

    let req =  this.http.delete(urls.server + 'Tasks/'+taskId,{body: isArhive});

    return req;


  }
}
