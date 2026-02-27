import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { MyTask, Status } from '../models/task.model';

@Injectable({ providedIn: 'root' })
export class TaskService {
  private tasksSubject = new BehaviorSubject<MyTask[]>([]);
  tasks$ = this.tasksSubject.asObservable();

  constructor() {
    // При старте приложения подставляем 5 примеров
    const initial: MyTask[] = [];
    this.tasksSubject.next(initial);
  }


  statuses: Status[] = [];


  changeTask(){


  }

  /** Получаем задачи по статусу */
  getTasksByStatus(status: Status): MyTask[] {
    return this.tasksSubject.getValue().filter(t => t.statusId === status.id).sort((a, b) => a.orderNumber - b.orderNumber);
  }


  // Изменение порядка
  updateTaskOrder(taskId: number, order: number){
    const tasks = this.tasksSubject.getValue().map(t =>
        t.id === taskId ? { ...t, orderNumber: order } : t
      );
      this.tasksSubject.next(tasks);
  }


  /** Перемещаем задачу в новый статус */
  updateTaskStatus(taskId: number, newStatus: Status | undefined) {
    if(newStatus){
      const tasks = this.tasksSubject.getValue().map(t =>
        t.id === taskId ? { ...t, statusId: newStatus.id } : t
      );
      this.tasksSubject.next(tasks);
    }
  }


  /** Добавляем задачу */
  addTask(task: Omit<MyTask, 'id'>) {
    const newTask: MyTask = { ...task, id: 0 };
    this.tasksSubject.next([...this.tasksSubject.getValue(), newTask]);
  }


  /** Удаляем задачу */
  deleteTask(taskId: number) {
    this.tasksSubject.next(
      this.tasksSubject.getValue().filter(t => t.id !== taskId)
    );
  }
}
