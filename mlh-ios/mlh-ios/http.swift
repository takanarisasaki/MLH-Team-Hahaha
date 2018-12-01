//
//  http.swift
//  mlh-ios
//
//  Created by Chen He on 2018-12-01.
//  Copyright © 2018 Chen He. All rights reserved.
//

import Foundation


class HTTP{
    
    typealias eventhandler = ([String:Any])  -> Void
    typealias failhandler = ()  -> Void
    public static func post(addr:String,data:[String:Any],f:@escaping eventhandler) -> Void {
        
        
        do {
            let url = URL(string: "https://mlh-team-hahaha.azurewebsites.net/\(addr)")
            let session = URLSession.shared
            
            var request = NSMutableURLRequest(url: url as! URL)
            request.httpMethod = "POST"
            request.addValue("application/json", forHTTPHeaderField: "Content-Type")
            request.httpBody = try JSONSerialization.data(withJSONObject: data, options: .prettyPrinted)
            
            let task = URLSession.shared.dataTask(with: request as URLRequest) { data, response, error in
                if error != nil {
                    return
                }
                guard let data = data, error == nil else {
                    return
                }
                let responseJSON = try? JSONSerialization.jsonObject(with: data, options: [])
                if let responsedic = responseJSON as? [String: Any] {
                    f(responsedic)
                }
            }
            
            task.resume()
        } catch  {
            print("failed")
        }
        
    }
    public static func post(addr:String,data:[String:Any],f:@escaping eventhandler,fail:@escaping failhandler) -> Void {
        
        
        do {
            let url = URL(string: "https://mlh-team-hahaha.azurewebsites.net/\(addr)")
            let session = URLSession.shared
            
            var request = NSMutableURLRequest(url: url as! URL)
            request.httpMethod = "POST"
            request.addValue("application/json", forHTTPHeaderField: "Content-Type")
            request.httpBody = try JSONSerialization.data(withJSONObject: data, options: .prettyPrinted)
            
            let task = URLSession.shared.dataTask(with: request as URLRequest) { data, response, error in
                if error != nil {
                    fail()
                    return
                }
                guard let data = data, error == nil else {
                    return
                }
                let responseJSON = try? JSONSerialization.jsonObject(with: data, options: [])
                if let responsedic = responseJSON as? [String: Any] {
                    f(responsedic)
                }
            }
            
            task.resume()
        } catch  {
            print("failed")
        }
        
    }
}
