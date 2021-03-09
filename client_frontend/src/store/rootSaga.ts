import {fork} from 'redux-saga/effects';
import {watcher as SignInSaga} from '../pages/SignIn/duck';
import {watcher as RegisterSaga} from '../components/forms/register/duck';
import {watcher as LookupSaga} from '../components/lookupSearch/duck';

export default function* rootSaga() {
  yield fork(SignInSaga);
  yield fork(LookupSaga);
  yield fork(RegisterSaga);
}