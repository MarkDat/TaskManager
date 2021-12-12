import notify from 'devextreme/ui/notify';
import {confirm, alert} from 'devextreme/ui/dialog';

export class AppNotify {
	static info(message: string, timeShown: number = 3000) {
		notify(message, 'info', timeShown);
	}

	static warning(message: string, timeShown: number = 3000) {
		notify(message, 'warning', timeShown);
	}

	static error(message: string, timeShown: number = 3000) {
		notify(message, 'error', timeShown);
	}

	static success(message?: string, timeShown: number = 3000) {
		notify(message || SUCCESSFULLY, 'success', timeShown);
	}

	static confirm(message: string, title: string): Promise<boolean> {
		return confirm(message, title);
	}

	static alert(message: string, title: string) {
		return alert(message, title);
	}
}

export const ERROR = 'An error has occurred!';
export const SUCCESSFULLY = 'Successfully done';
export const SUBMITSUCCESS = 'Submitted successfully!';
export const RESUBMITSUCCESS = 'Resubmitted successfully!';
export const ADDSUCCESS = 'Add successfully!';
export const UPDATESUCCESS = 'Updated successfully!';
export const DELETESUCCESS = 'Deleted successfully';
export const NOAUTHORIZED = 'You are not authorized!';
export const UPDATEUNSUCCESS = 'Error when Updating.';
