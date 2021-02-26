import {fork} from 'redux-saga/effects';
import {watcher as SignInSaga} from '../pages/SignIn/duck';
import {watcher as RegisterSaga} from '../components/forms/register/duck';

export default function* rootSaga() {
  yield fork(SignInSaga);
  yield fork(RegisterSaga);
}