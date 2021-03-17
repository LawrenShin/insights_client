import {fork} from 'redux-saga/effects';
import {watcher as signInSaga} from '../pages/SignIn/duck';
import {watcher as resultsSaga} from '../pages/results/duck';
import {watcher as registerSaga} from '../components/forms/register/duck';
import {watcher as lookupSaga} from '../components/lookupSearch/duck';

export default function* rootSaga() {
  yield fork(signInSaga);
  yield fork(lookupSaga);
  yield fork(registerSaga);
  yield fork(resultsSaga);
}