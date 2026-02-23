import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Task, TaskStatus } from '../models/task.model';

@Injectable({ providedIn: 'root' })
export class TaskService {
  private tasksSubject = new BehaviorSubject<Task[]>([]);
  tasks$ = this.tasksSubject.asObservable();

  constructor() {
    // При старте приложения подставляем 5 примеров
    const initial: Task[] = [
      { id: 1, orderNumber: 1, title: 'Создать модель', status: TaskStatus.TODO, description: "Описание чего нужно сделать, длинное муторное описание ...." },
      { id: 2, orderNumber: 1, title: 'Подготовить UI', status: TaskStatus.TODO },
      { id: 3, orderNumber: 1, title: 'Написать сервис', status: TaskStatus.IN_PROGRESS },
      { id: 4, orderNumber: 1, title: 'Тестировать', status: TaskStatus.IN_PROGRESS },
      { id: 5, orderNumber: 1, title: 'Развернуть', status: TaskStatus.DONE },
      { id: 6, orderNumber: 1, title: '6', status: TaskStatus.TODO },
      { id: 7, orderNumber: 1, title: '7', status: TaskStatus.TODO },
      { id: 8, orderNumber: 1, title: '8', status: TaskStatus.TODO },
      { id: 9, orderNumber: 1, title: '9', status: TaskStatus.TODO }
    ];
    this.tasksSubject.next(initial);
  }

  changeTask(){


  }

  /** Получаем задачи по статусу */
  getTasksByStatus(status: TaskStatus): Task[] {
    return this.tasksSubject.getValue().filter(t => t.status === status).sort((a, b) => a.orderNumber - b.orderNumber);
  }

  // Изменение порядка
  updateTaskOrder(taskId: number, order: number){
    const tasks = this.tasksSubject.getValue().map(t =>
        t.id === taskId ? { ...t, orderNumber: order } : t
      );
      this.tasksSubject.next(tasks);
  }

  /** Перемещаем задачу в новый статус */
  updateTaskStatus(taskId: number, newStatus: TaskStatus | undefined) {
    if(newStatus){
      const tasks = this.tasksSubject.getValue().map(t =>
        t.id === taskId ? { ...t, status: newStatus } : t
      );
      this.tasksSubject.next(tasks);
    }
  }

  /** Добавляем задачу */
  addTask(task: Omit<Task, 'id'>) {
    const newTask: Task = { ...task, id: 0 };
    this.tasksSubject.next([...this.tasksSubject.getValue(), newTask]);
  }

  /** Удаляем задачу */
  deleteTask(taskId: number) {
    this.tasksSubject.next(
      this.tasksSubject.getValue().filter(t => t.id !== taskId)
    );
  }
}
