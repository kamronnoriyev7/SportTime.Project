import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { Task, TaskPriority, TaskCategory, TaskPriorityLabels, TaskCategoryLabels } from '../../models/task.model';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.scss']
})
export class TaskFormComponent implements OnInit {
  taskForm: FormGroup;
  loading = false;
  error = '';
  isEditMode = false;
  taskId: number | null = null;
  
  // Enums for template
  TaskPriority = TaskPriority;
  TaskCategory = TaskCategory;
  TaskPriorityLabels = TaskPriorityLabels;
  TaskCategoryLabels = TaskCategoryLabels;

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.taskForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      description: ['', [Validators.maxLength(1000)]],
      dueDate: [''],
      priority: [TaskPriority.Medium, [Validators.required]],
      category: [TaskCategory.General, [Validators.required]],
      isCompleted: [false]
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['id'] && params['id'] !== 'new') {
        this.isEditMode = true;
        this.taskId = +params['id'];
        this.loadTask();
      }
    });
  }

  loadTask(): void {
    if (!this.taskId) return;
    
    this.loading = true;
    this.taskService.getTask(this.taskId).subscribe({
      next: (task) => {
        this.taskForm.patchValue({
          title: task.title,
          description: task.description,
          dueDate: task.dueDate ? new Date(task.dueDate).toISOString().split('T')[0] : '',
          priority: task.priority,
          category: task.category,
          isCompleted: task.isCompleted
        });
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Vazifani yuklashda xatolik yuz berdi';
        this.loading = false;
        console.error('Vazifani yuklashda xatolik:', error);
      }
    });
  }

  onSubmit(): void {
    if (this.taskForm.valid) {
      this.loading = true;
      this.error = '';
      
      const formValue = this.taskForm.value;
      const taskData = {
        title: formValue.title,
        description: formValue.description,
        dueDate: formValue.dueDate ? new Date(formValue.dueDate) : undefined,
        priority: formValue.priority,
        category: formValue.category,
        ...(this.isEditMode && { isCompleted: formValue.isCompleted })
      };

      const request = this.isEditMode && this.taskId
        ? this.taskService.updateTask(this.taskId, taskData as any)
        : this.taskService.createTask(taskData);

      request.subscribe({
        next: () => {
          this.router.navigate(['/dashboard']);
        },
        error: (error) => {
          this.error = this.isEditMode 
            ? 'Vazifani yangilashda xatolik yuz berdi'
            : 'Vazifani yaratishda xatolik yuz berdi';
          this.loading = false;
          console.error('Vazifa bilan ishlashda xatolik:', error);
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/dashboard']);
  }

  get pageTitle(): string {
    return this.isEditMode ? 'Vazifani tahrirlash' : 'Yangi vazifa';
  }

  get submitButtonText(): string {
    return this.isEditMode ? 'Yangilash' : 'Yaratish';
  }
}