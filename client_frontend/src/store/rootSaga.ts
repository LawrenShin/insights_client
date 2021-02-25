import {fork} from 'redux-saga/effects';
import {watcher as SignInSaga} from '../pages/SignIn/duck';

export default function* rootSaga() {
  yield fork(SignInSaga);
}