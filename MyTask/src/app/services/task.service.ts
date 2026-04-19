import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { MyTask, Status, TypeTask } from '../models/task.model';
import { HttpClient } from '@angular/common/http';
import { TimeSpentDTO } from '../models/TimeSpentDTO';
import { error } from 'console';
import { AppSettings } from '../../app.settings';
import { Executor } from '../models/Executor';

@Injectable({ providedIn: 'root' })
export class TaskService {
  public tasksSubject = new BehaviorSubject<MyTask[]>([]);
  tasks$ = this.tasksSubject.asObservable();

  constructor() {
    // При старте приложения подставляем 5 примеров
    const initial: MyTask[] = [];
    this.tasksSubject.next(initial);
    this.GetListStatuses();
    this.GetTypeTasks();
    this.GetListExecutors();
  }

  private http = inject(HttpClient);

  statuses: Status[] = [];
  types: TypeTask[] = [];
  executors: Executor[] = [];

  GetListExecutors(){
    this.http.get<Executor[]>(AppSettings.env_vars.API_URL + 'Executors').subscribe(
      date=>{
        this.executors = date;
      }
    );
  }


  GetListStatuses(){
    this.http.get<Status[]>(AppSettings.env_vars.API_URL + 'TaskStatus').subscribe(
      date=>{
        this.statuses = date;
      }
    );
  }

  SaveStatus(status: Status){
    return this.http.put<Status>(AppSettings.env_vars.API_URL+'TaskStatus', status);
  }


  getStatuses(): Status[]{
    return this.statuses.filter(t=>t.isHidden !== true)


  }

  GetListTasks(){
    let req =  this.http.get<MyTask[]>(AppSettings.env_vars.API_URL + 'Tasks')
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

  GetTypeTasks(){
    let t = this.http.get<TypeTask[]>(AppSettings.env_vars.API_URL+'TaskTypes')
    t.subscribe(
      (data) =>{
        this.types = data;
      },
      (error)=>{
        console.log(error);
      }
    );
  }

  SaveTypeTask(type: TypeTask){
    return this.http.post<TypeTask>(AppSettings.env_vars.API_URL+'TaskTypes', type);
  }

  SaveEx(ex: Executor){
    return this.http.post<Executor>(AppSettings.env_vars.API_URL+'Executors', ex);
  }

  AddTypeTask(){
    let t = new TypeTask();
    t.id = 0;
    t.name = "Новый тип"
    t.color = "#fff"
    this.types.push(t);
  }


  AddEx(){
    let t = new Executor();
    t.id = 0;
    t.name = "Новый"
    this.executors.push(t);
  }

  deleteTypeTask(taskId: number){
    return this.http.delete(AppSettings.env_vars.API_URL + 'TaskTypes/'+taskId);
  }


  getFixedTasks():MyTask[] {
    return this.tasksSubject.getValue().filter(t=>t.isFixed === true)
    .sort((a, b) => a.orderNumber - b.orderNumber);
  }

  haveFixedTasks(): boolean {
    return this.tasksSubject.getValue().findIndex(t=>t.isFixed === true) !== -1;
  }

  /** Получаем задачи по статусу */
  getTasksByStatus(status: Status): MyTask[] {
    return this.tasksSubject.getValue().filter(t => t.statusId === status.id && t.isFixed !== true).sort((a, b) => a.orderNumber - b.orderNumber);
  }

  getTypeTaskName(typeId: number | null | undefined){
    return this.types.find(i=>i.id == typeId)?.name;
  }

  getTypeTaskColor(typeId: number | null | undefined){
    return this.types.find(i=>i.id == typeId)?.color;
  }

  AddTime(taskId: number, time: number){
    let t = new TimeSpentDTO();

    t.taskId = taskId;
    t.duration = time;

    return this.http.post<number>(AppSettings.env_vars.API_URL + 'TimeSpent', t);

  }

  updateTaskOrder(data: any[]){

    console.log(data);

    let tasks = data.map((i, index) => ({
      taskId: i.id,
      order: index
     }));

    this.http.post(AppSettings.env_vars.API_URL+"OrderTasks", tasks).subscribe(
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

      this.http.post(AppSettings.env_vars.API_URL+"ChangeState?TaskId="+taskId+"&StateId="+newStatus.id,null).subscribe(
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

    let req =  this.http.post<MyTask>(AppSettings.env_vars.API_URL + 'Tasks', newTask);


    return req;
  }

  updateTask(task: MyTask){
    let req =  this.http.put<MyTask>(AppSettings.env_vars.API_URL + 'Tasks/'+task.id, task);

    return req;
  }


  /** Удаляем задачу */
  deleteTask(taskId: number, isArhive: boolean) {

    let req =  this.http.delete(AppSettings.env_vars.API_URL + 'Tasks/'+taskId,{body: isArhive});

    return req;
  }
}
