import {combineReducers} from "redux";
import {reducer as SignIn} from '../pages/SignIn/duck';
import {reducer as Register} from '../components/forms/register/duck';

const rootReducer = combineReducers({
  SignIn,
  Register,
});

export type RootState = ReturnType<typeof rootReducer>

export default rootReducer;
