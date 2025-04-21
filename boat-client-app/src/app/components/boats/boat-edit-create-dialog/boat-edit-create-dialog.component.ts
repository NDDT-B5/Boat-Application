import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { BoatDeleteDialogComponent } from '../boat-delete-dialog/boat-delete-dialog.component';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { BoatDto } from '../../../shared/models/boat.model';
import { SnackbarService } from '../../../core/services/snackbar.service';

export interface BoatEditCreateDialogData {
  mode: 'add' | 'edit';
  boat: BoatDto;
}

@Component({
  selector: 'app-boat-edit-create-dialog',
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    CommonModule,
    MatIconModule
  ],
  templateUrl: './boat-edit-create-dialog.component.html',
  styleUrl: './boat-edit-create-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BoatEditCreateDialogComponent implements OnInit {
  isEdit: boolean = true;
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: BoatEditCreateDialogData, 
    private dialogRef: MatDialogRef<BoatDeleteDialogComponent>,
    private snackBarService: SnackbarService
  ) {}

  ngOnInit(): void {
    this.isEdit = this.data.mode === "edit";

    this.form = this.fb.group({
      name: [
        this.data.boat?.name || '',
        [Validators.required, Validators.minLength(1), Validators.maxLength(100)]
      ],
      description: [this.data.boat?.description || '', Validators.maxLength(500)],
      id: [{ value: this.data.boat?.id ?? '', disabled: true }],
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formData = { ...this.form.value, id: this.data.boat?.id };
      this.dialogRef.close(formData);
    } else {
      this.snackBarService.showError('Please fix the errors in the form before submitting.');
      this.form.markAllAsTouched();
    }
  }
}
