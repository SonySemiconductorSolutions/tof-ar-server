/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2022 Sony Semiconductor Solutions Corporation.
 *
 */

#import <Foundation/Foundation.h>

#ifdef __cplusplus
extern "C" {
#endif

int thermalState()
{
    return (int) [NSProcessInfo.processInfo thermalState];
}

#ifdef __cplusplus
}
#endif
