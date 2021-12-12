import {custom} from 'devextreme/ui/dialog';

export class CommonFunction{
    public static confirmDialogPromise(titleStr: string, messageStr: string, nameYes = 'YES', nameNo = 'NO', visibleNoBtn = true): Promise<boolean> {
		return new Promise((resolve, reject) => {
			const confirmDialog = custom({
				title: titleStr,
				message: messageStr,
				buttons: [{
					text: nameYes,
					height: 40,
					type: 'default',
					onClick: () => {
						resolve(true);
					}
				}, {
					text: nameNo,
					visible: visibleNoBtn,
					height: 40,
					onClick: () => {
						confirmDialog.hide();
						resolve(false);
					}
				}]
			});

			confirmDialog.show();
		});
	}
}