import {FormControl, IconButton, InputAdornment, InputLabel, OutlinedInput, FormHelperText} from "@material-ui/core";
import {Visibility, VisibilityOff} from "@material-ui/icons";
import React from "react";
import {useField} from "formik";
import { at } from 'lodash';


export default ({
  variant,
  size,
  ...props
  }: any) => {
  const [showPassword, setShowPassword] = React.useState<boolean>(false);
  const handleClickShowPassword = () => setShowPassword(!showPassword);
  const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
  };

  const { errorText, ...rest } = props;
  const [field, meta] = useField(props);
  const [touched, error] = at(meta, 'touched', 'error');

  function _renderHelperText() {
    if (touched && error?.password) {
      return <FormHelperText error={touched && error?.password}>{error?.password}</FormHelperText>
    }
    return<></>
  }

  return (
    <FormControl
      variant={variant}
      size={size}
    >
      <InputLabel htmlFor="password">Password</InputLabel>
      <OutlinedInput
        {...field}
        {...rest}
        error={touched && error?.password}
        name={'password'}
        type={showPassword ? 'text' : 'password'}
        endAdornment={
          <InputAdornment position="end">
            <IconButton
              aria-label="toggle password visibility"
              onClick={handleClickShowPassword}
              onMouseDown={handleMouseDownPassword}
              edge="end"
            >
              {showPassword ? <Visibility/> : <VisibilityOff/>}
            </IconButton>
          </InputAdornment>
        }
        labelWidth={70}
      />
      {_renderHelperText()}
    </FormControl>
  )
}