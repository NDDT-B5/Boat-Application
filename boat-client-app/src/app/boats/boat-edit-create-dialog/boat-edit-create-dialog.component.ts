import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { BoatsItem } from '../boats-datasource';
import { CommonModule } from '@angular/common';

export interface BoatEditCreateDialogData {
  mode: 'add' | 'edit';
  boat: BoatsItem;
}

@Component({
  selector: 'app-boat-edit-create-dialog',
  imports: [MatDialogModule, MatButtonModule, CommonModule],
  templateUrl: './boat-edit-create-dialog.component.html',
  styleUrl: './boat-edit-create-dialog.component.scss'
})
export class BoatEditCreateDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: BoatEditCreateDialogData) {}
}
