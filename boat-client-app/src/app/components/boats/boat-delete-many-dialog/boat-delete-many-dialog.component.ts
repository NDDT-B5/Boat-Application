import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { BoatDto } from '../../../shared/models/boat.model';
import { MatListModule } from '@angular/material/list';

export interface BoatDeleteDialogData {
  boats: BoatDto[];
}

@Component({
  selector: 'app-boat-delete-many-dialog',
  imports: [
    MatDialogModule,
    MatButtonModule,
    CommonModule,
    MatIconModule,
    MatListModule
  ],
  templateUrl: './boat-delete-many-dialog.component.html',
  styleUrl: './boat-delete-many-dialog.component.scss'
})
export class BoatDeleteManyDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: BoatDeleteDialogData, 
    private dialogRef: MatDialogRef<BoatDeleteManyDialogComponent>
  ) {}

  onDelete() {
    this.dialogRef.close(true);
  }

  onCancel() {
    this.dialogRef.close(false);
  }
}