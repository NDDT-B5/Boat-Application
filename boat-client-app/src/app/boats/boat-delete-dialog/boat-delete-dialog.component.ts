import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { BoatDto } from '../../core/models/boat.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';

export interface BoatDeleteDialogData {
  boat: BoatDto;
}

@Component({
  selector: 'app-boat-delete-dialog',
  imports: [
    MatDialogModule,
    MatButtonModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule
  ],
  templateUrl: './boat-delete-dialog.component.html',
  styleUrl: './boat-delete-dialog.component.scss'
})
export class BoatDeleteDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: BoatDeleteDialogData, 
    private dialogRef: MatDialogRef<BoatDeleteDialogComponent>
  ) {}

  onDelete() {
    this.dialogRef.close(true);
  }
}