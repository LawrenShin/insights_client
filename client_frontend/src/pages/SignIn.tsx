import React from 'react';

import Button from "../components/button";
import DiSvg from "../components/DiSvg";

import {
  Box,
  FormControl,
  IconButton,
  InputAdornment,
  InputLabel,
  OutlinedInput,
  TextField
} from '@material-ui/core';
import {VisibilityOff} from "@material-ui/icons";
import useStyles from "./useStyles";


export default () => {
  const styles = useStyles();

  return (
    <Box className={styles.root}>
      <Box className={styles.signInContainer}>
        <DiSvg styles={styles.DiSvgContainer} />
        <Box className={`${styles.inputs} ${styles.fullWidth}`}>
          <TextField
            size={'small'}
            id="login"
            label="Login"
            type="login"
            autoComplete="current-login"
            variant="outlined"
          />
          <FormControl
            style={{textAlign: 'right'}}
            // className={clsx(classes.margin, classes.textField)}
            variant="outlined"
            size={'small'}
          >
            <InputLabel htmlFor="password">Password</InputLabel>
            <OutlinedInput
              id="password"
              // type={values.showPassword ? 'text' : 'password'}
              type={'password'}
              // value={values.password}
              // onChange={handleChange('password')}
              endAdornment={
                <InputAdornment position="end">
                  <IconButton
                    aria-label="toggle password visibility"
                    // onClick={handleClickShowPassword}
                    // onMouseDown={handleMouseDownPassword}
                    edge="end"
                  >
                    {/*{values.showPassword ? <Visibility /> : <VisibilityOff />}*/}
                    <VisibilityOff />
                  </IconButton>
                </InputAdornment>
              }
              labelWidth={70}
            />
            <a href={'#'}>forgot password?</a>
          </FormControl>
        </Box>
        <Box className={`${styles.fullWidth} ${styles.marginAuto}`}>
          <Button style={styles.button}>Sign in</Button>
          <Button style={`${styles.button} ${styles.whiteBack}`} >Register</Button>
        </Box>
      </Box>
    </Box>
  );
}
